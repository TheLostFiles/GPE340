using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    public float Health = 10;
    public GameObject head;
    public GameObject body;
    public GameObject arms;
    public Collider dummyCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Sets all of the variables to the correct ones.
        head = GameObject.Find("Head");
        body = GameObject.Find("Body");
        arms = GameObject.Find("Arms");
        dummyCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            // When the dummy "dies" all of its objects deactvated
            head.SetActive(false);
            body.SetActive(false);
            arms.SetActive(false);
            dummyCollider.enabled = false;
            // Starts Coroutine
            StartCoroutine(Respawn());
        }
    }

    public IEnumerator Respawn()
    {
        // Makes delay happen.
        yield return new WaitForSeconds(5);
        // Sets health back to ten
        Health = 10;
        // Reenables the objects
        dummyCollider.enabled = true;
        head.SetActive(true);
        body.SetActive(true);
        arms.SetActive(true);
    }

    // Checks to see if something hits it
    private void OnTriggerEnter(Collider other)
    {
        // Checks to see if it has the tag Bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Makes it lose health
            Health -= 1;
            // Destorys the bullet
            Destroy(other.gameObject);
        }
    }
       
    
}
