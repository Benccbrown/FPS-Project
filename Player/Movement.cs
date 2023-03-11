using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Allows for movement with collisions without the need of a rigidbody
    public CharacterController Controller;

    //Movement variables
    public float Speed = 7f;
    public float Gravity = -10f;
    public float JumpH = 2f;

    //Death Script
    public float health = 100f;
    public GameObject Player;
    public GameObject Enemy;
    public bool dead = false;
    public AI ai;
    public Shoot shoot;
    public GameObject PlayerDeathText;
    public GameObject Panel;

    //Jumping variables
    public Transform PlayerGroundCheck;
    public LayerMask GroundLayer;
    public float Distance = 0.5f;
    Vector3 Velocity;
    bool Grounded;

    //When the script is called, the death text box does not show
    private void Awake()
    {
        PlayerDeathText.SetActive(false);
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded happens when the position of the object tied to PlayerGroundCheck is 0.5f from the layer of gorund
        Grounded = Physics.CheckSphere(PlayerGroundCheck.position, Distance, GroundLayer);

        //If the player is grounded with a y velocity lower than 0
        if (Grounded && Velocity.y < 0)
        {
            Velocity.y = -3f;
        }

        //Gets the current horizontal and vertical axis
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        //allows the user to move to the desired areas every second
        Vector3 Move = transform.right * x + transform.forward * z;
        Controller.Move(Move * Speed * Time.deltaTime);
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);

        //If the player is grounded and presses space, their velocity goes up 
        if (Input.GetButtonDown("Jump") && Grounded)
            Velocity.y = Mathf.Sqrt(JumpH * -2f * Gravity);
    }

    //This is triggered when the enemy bullet collides with the player
    public void TakeDamage(int damage)
    {
        //Health is reduced based on the bullet damage
        health -= damage;
        HealthLeft.health = (int)health; //Prints the current Health for the UI

        //if the health is less than or equal to 0
        if (health <= 0)
        {
            //move the player and AI back to the spawns
            Player.transform.position = new Vector3(-10, 5, 0);
            Enemy.transform.position = new Vector3(10, 5, 0);
            //Run EnemyWin
            EnemyWin();
            dead = true;
            //Run Player Lost method
            StartCoroutine(PlayerLost());
            ai.Nerf();
        }
    }
    //This runs when the Enemy Wins
    public void EnemyWin()
    {
        //The players and AI stats are reset
        health = 100;
        HealthLeft.health = (int)health; //Prints the current Health for the UI
        ai.BulletsLeft = 12 + ai.ExtraBullet;
        ai.EnemyHP = 100;
        shoot.BulletsLeft = 12;
        AmmoLeft.ammo = (int)shoot.BulletsLeft; //current Ammo for the UI
    }
    //Run the player lost text for one second
    IEnumerator PlayerLost()
    {
        //sets the text and panel to true
        PlayerDeathText.SetActive(true);
        Panel.SetActive(true);
        //wait one second
        yield return new WaitForSeconds(1);
        //sets the text and panel to true
        PlayerDeathText.SetActive(false);
        Panel.SetActive(false);
    }
}
