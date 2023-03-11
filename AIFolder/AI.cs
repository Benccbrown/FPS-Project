using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Target;

    public LayerMask GroundLayer, TargetLayer;

    //Death Script 
    public float EnemyHP;
    public GameObject Player;
    public GameObject Enemy;
    public bool dead = false;
    public Movement movement;
    public Shoot shoot;
    public GameObject EnemyDeathText;
    public GameObject Panel;
    public static int difficulty = 1;
    public int lives = 3;
    Look mouseRel = new Look();


    //Patroling
    public Vector3 WalkTarget;
    bool WalkTargetSet;
    public float WalkTargetDistance;

    //Attacking
    public float AttackInterval;
    public bool Shooting = false;
    public Rigidbody EnemyBullet;
    public float BulletSpeed = 50;
    public float BulletsLeft = 6;
    public int ExtraDmg = 0;
    public int ExtraBullet = 0;
    public float ExtraAttackSpeed = 0;

    //States for the AI
    public float Vision, Range;
    public bool TargetVisible, TargetClose;

    //When the script begins
    private void Awake()
    {
        //find the player and set the agent
        Target = GameObject.Find("Player").transform;
        Agent = GetComponent<NavMeshAgent>();
        EnemyDeathText.SetActive(false);
    }

    private void Update()
    {
        //Check for sight and attack range
        TargetVisible = Physics.CheckSphere(transform.position, Vision, TargetLayer);
        TargetClose = Physics.CheckSphere(transform.position, Range, TargetLayer);

        //decides if the AI patrols, chases or attacks the player to run their methods
        if (!TargetVisible && !TargetClose) Patroling();
        if (TargetVisible && !TargetClose) ChasePlayer();
        if (TargetVisible && TargetClose) AttackPlayer();
    }

    private void Patroling()
    {
        //If there is no walk target find one
        if (!WalkTargetSet) SearchWalkTarget();

        //if there is a walk target
        if (WalkTargetSet)
           //the ai goes to that target
            Agent.SetDestination(WalkTarget);

        //Find the distance to the target
        Vector3 DistanceToWalkTarget = transform.position - WalkTarget;

        //Walkpoint reached
        if (DistanceToWalkTarget.magnitude < 1f)
            //walk target is false so a new target will be found
            WalkTargetSet = false;
    }
    private void SearchWalkTarget()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-WalkTargetDistance, WalkTargetDistance);
        float randomX = Random.Range(-WalkTargetDistance, WalkTargetDistance);

        //Decides on a random target to walk to
        WalkTarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Decides on if the target is possible
        if (Physics.Raycast(WalkTarget, -transform.up, 2f, GroundLayer))
            WalkTargetSet = true;
    }

    private void ChasePlayer()
    {
        //Follows the player instead of going to a destination
        Agent.SetDestination(Target.position);
    }

    private void AttackPlayer()
    {
        //Makes sure enemy doesn't move
        Agent.SetDestination(transform.position);

        transform.LookAt(Target);

        //relaods if there are no bullets
        if (BulletsLeft == 0)
        {
            StartCoroutine(Reload());
        }

        //if not currently shooting, start section of shooting the gun
        if (Shooting == false)
        {
            if (BulletsLeft > 0)
            {
                StartCoroutine(ShootGun());
            }
        }
    }

    IEnumerator ShootGun()
    {
        Shooting = true;
        //The enemy bullet is instanced and gains force forward and upwards
        Rigidbody InstancedBullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity) as Rigidbody;
        InstancedBullet.AddForce(transform.forward * 32f, ForceMode.Impulse);
        InstancedBullet.AddForce(transform.up * 4f, ForceMode.Impulse);

        //Wait three seconds for the next shot plus the attack speed
        yield return new WaitForSeconds(3.0f+ExtraAttackSpeed); //waits for 0.5 seconds
        //no longer shooting
        Shooting = false;
        BulletsLeft--;
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f); //waits for 1 second
        BulletsLeft = 6 + ExtraBullet;
    }
    //The AI take damage script
    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
        //If the enemy hp is less than or equal to 0
        if (EnemyHP <= 0)
        {
            //both the player and AI are transformed to their spawns
            Player.transform.position = new Vector3(-10, 5, 0);
            Enemy.transform.position = new Vector3(10, 5, 0);
            //run the player win method
            PlayerWin();
            dead = true;
            //run the enemy lost method
            StartCoroutine(EnemyLost());
            //run the buff method
            Buff();
        }
    }
    private void PlayerWin()
    {
        EnemyHP = 100;
        BulletsLeft = 6 + ExtraBullet;
        movement.health = 100;
        shoot.BulletsLeft = 12;
        HealthLeft.health = (int)movement.health; //Prints the current Health for the UI
        AmmoLeft.ammo = (int)shoot.BulletsLeft; //current Ammo for the UI
    }
    IEnumerator EnemyLost()
    {
        EnemyDeathText.SetActive(true);
        Panel.SetActive(true);
        yield return new WaitForSeconds(1);
        EnemyDeathText.SetActive(false);
        Panel.SetActive(false);
    }
    public void Nerf()
    {
        //To avoid having 0 bullets they only get nerfed when bullets are more than -5
        if (ExtraBullet > -5)
        {
            //The AI is weakened along with the difficulty being decreased
            difficulty--;
            DifficultyUI.difficulty = (int)difficulty; //current Difficulty for the UI
            ExtraDmg--;
            ExtraBullet--;
            ExtraAttackSpeed = ExtraAttackSpeed + 0.1f;
            lives--;
            LivesLeft.lives = (int)lives; //current lives for the UI

            //Resets the targets patrol destination to allow it to be random
            WalkTargetSet = false;

            if (lives == 0)
            {
                //Release the mouse when the player has no lives
                mouseRel.MouseRelease();
                //Load the end scene
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            //it is impossible to reach but if there are more than 3 lives, this could be an option
            print("Lowest Difficulty Reached");
        }
    }
    public void Buff()
    {
        //The AI is strengthened along with the difficulty being increased
        difficulty++;
        DifficultyUI.difficulty = (int)difficulty; //current Difficulty for the UI
        ExtraDmg++;
        ExtraBullet++;
        ExtraAttackSpeed = ExtraAttackSpeed - 0.1f;
        //Resets the targets patrol destination to allow it to be random
        WalkTargetSet = false;
        //increases the maximum difficulty for the last scene
        if (difficulty > FinalDifficulty.diff)
        {
            FinalDifficulty.diff = difficulty;
        }
    }

    //For the editor on unity to see the range and vision when not in game
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Vision);
    }
}
