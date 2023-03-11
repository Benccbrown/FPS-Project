using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    bool hit = false;

    //When starting, the bullet will be destroyed 2 seconds later to avoid a cluster of items
    void Start()
    {
        Destroy(gameObject, 2);
    }
    //On collision
    private void OnCollisionEnter(Collision collision)
    {
        //if the bullet has not hit to avoid multiple collisions at once
        if (hit == false)
        {
            //if the bullet collides with objects with the EnemyBody tag
            if (collision.transform.tag == "EnemyBody")
            {
                //deal random damage from 10-18
                int dmg = Random.Range(10, 18);
                //starts the TakeDamage function on the AI script
                collision.gameObject.GetComponent<AI>().TakeDamage(dmg);
                //Hit is true to avoid another collision
                hit = true;
            }
            //the object is destroyed after collision with any object
            Destroy(this.gameObject);
        }
    }
}
