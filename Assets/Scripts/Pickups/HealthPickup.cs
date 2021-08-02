using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{

    public float amountToHeal;

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
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Grabs the health componant.
            Health healthComponent = other.GetComponent<Health>();
            // Runs Heal.
            healthComponent.Heal(amountToHeal);
            // Runs OnPickup.
            base.OnPickup();
        }
        
    }
}
