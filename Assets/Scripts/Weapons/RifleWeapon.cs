using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    public float fireRate;
    private float nextFire = 0;


    public override void OnTriggerHold()
    {
        // Timer for fire speed.
        isTriggerPulled = true;
        if(Time.time > nextFire)
        {
            // More Fire rate.
            nextFire = Time.time + fireRate;
            // Run Shoot.
            Shoot();
        }
    }

    public override void OnTriggerPull()
    {
        
    }

    public override void OnTriggerRelease()
    {
        isTriggerPulled = false;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    { 

    }


}
