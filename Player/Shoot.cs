using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public bool Shooting = false;
    public Rigidbody Bullet;
    public float BulletSpeed = 50;
    public float BulletsLeft = 12;

    // Update is called once per frame
    void Update()
    {
        //if there are no bullets left
        if (BulletsLeft == 0)
        {
            //start reloading
            StartCoroutine(Reload());
        }
        //if the user does left mouse click
        if (Input.GetButtonDown("Fire1"))
        {
            //if not currently shooting
            if (Shooting == false)
            {
                //if bullets are more than 0, the shoot method runs
                if (BulletsLeft > 0)
                {
                    StartCoroutine(ShootGun());
                }
            }
        }
    }
    IEnumerator ShootGun()
    {
        //the user is shooting
        Shooting = true;
        //a bullet is instanced with velocity to keep the bullet in the air and moves it forward based on the bullet speed
        Rigidbody InstancedBullet = Instantiate(Bullet, transform.position, transform.rotation) as Rigidbody;
        InstancedBullet.velocity = transform.TransformDirection(new Vector3(0, 0, BulletSpeed));

        //wait 0.2 seconds until the user can shoot again
        yield return new WaitForSeconds(0.2f); //waits for 0.5 seconds
        Shooting = false;
        BulletsLeft--;
        AmmoLeft.ammo --; //reduces the ammo by 1 on the UI
    }
    //Bullets are reset to 12 from reloading
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f); //waits for 1 second
        BulletsLeft = 12;
        AmmoLeft.ammo = 12; //Makes the ammo equal 12 on the UI
    }
}
