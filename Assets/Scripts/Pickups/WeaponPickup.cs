using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public override void OnPickup()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // Gets the pawn.
        Pawn pawnComponent = other.GetComponent<Pawn>();
        // Makes sure the rifle can be swapped to.
        pawnComponent.hasRifle = true;
        // Switches to the rifle on picking it up.
        pawnComponent.EquipWeapon(pawnComponent.pistol.GetComponent<PistolWeapon>());
        // Runs OnPickup.
        OnPickup();
    }
}
