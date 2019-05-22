using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{ }

    // Update is called once per frame
    //void Update()
    //{  }

    public Transform muzzle; //point that shoots projectile
    public Projectile projectile;
    public float msBetweenShots = 100; //miliseconds between each shot
    public float muzzleVelocity = 35; //speed blullet leaves the gun

    float nextShotTime; 

    public void Shoot()
    {
        //will only shoots when current time > next shot time
        if(Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
        }

        //instantiate bullet
        Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
        newProjectile.SetSpeed(muzzleVelocity); //mullet leaves gun at this velocity
    }
}
