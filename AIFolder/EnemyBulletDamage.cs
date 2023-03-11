using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDamage : MonoBehaviour
{
    public AI ai;
    bool hit = false;

    //When starting, the bullet will be destroyed 2 seconds later to avoid a cluster of items, the bullet also comes from the AI and they find the player
    void Start()
    {
        ai = GameObject.Find("Enemy").GetComponent<AI>();
        Destroy(gameObject, 2);
    }
    //On collision
    private void OnCollisionEnter(Collision collision)
    {
        //if the bullet has not hit to avoid multiple collisions at once
        if (hit == false)
        {
            //if the bullet collides with objects with the PlayerBody tag
            if (collision.transform.tag == "PlayerBody")
            {
                //deal random damage from 3-6 plus the extra damage created
                int dmg = Random.Range(3, 6) + ai.ExtraDmg;
                // starts the TakeDamage function on the Monvement script
                collision.gameObject.GetComponent<Movement>().TakeDamage(dmg);
                //Hit is true to avoid another collision
                hit = true;
            }
            //the object is destroyed after collision with any object
            Destroy(this.gameObject);
        }
    }
}
