using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Weapon
{
    public override void OnTriggerPull()
    {
        // Calls Shoot.
        Shoot();
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
        base.Update();
    }
}
