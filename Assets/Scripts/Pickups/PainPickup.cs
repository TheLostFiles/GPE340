using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainPickup : Pickup
{

    public float amountToHurt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Checks to see if something hits it
    public void OnTriggerEnter(Collider other)
    {
        // Grabs the health component.
        Health healthComponent = other.GetComponent<Health>();
        // Runs hurt.
        healthComponent.Hurt(amountToHurt);
        base.OnPickup();
    }
}
