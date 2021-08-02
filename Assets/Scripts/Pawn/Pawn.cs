using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Pawn : MonoBehaviour
{
    [HideInInspector] public Animator anim;

    public float speed;
    public float turnSpeed = 180;
    
    public Weapon weapon;
    public Health health;

    public GameObject pistol;
    public GameObject rifle;    
    


    public bool hasRifle;

    private Collider topCollider;
    private Rigidbody topRigidbody;
    public List<Collider> ragdollColliders;
    public List<Rigidbody> ragdollRigidbodies;



    public PlayerController pc;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        topCollider = GetComponent<Collider>();
        topRigidbody = GetComponent<Rigidbody>();

        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        pc = GetComponent<PlayerController>();

        StopRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if health is zero
        if(health.currentHealth <= 0)
        {
            isDead = true; 
            StartCoroutine(Die());
        }
    }

    // Pickup and swapping of weapons for players
    public void EquipWeapon(Weapon weaponSwap)
    {
        // Checks if the has rifle bool is true.
        if (hasRifle)
        {
            // If the weapon is a pistol and you scroll this switchs it to the rifle.
            if(weaponSwap == pistol.GetComponent<PistolWeapon>())
            {
                pistol.SetActive(false);
                rifle.SetActive(true);
                pc.pistolImage.SetActive(false);
                pc.rifleImage.SetActive(true);

                weapon = rifle.GetComponent<RifleWeapon>();
            }
        }
        // If the weapon is a rifle and you scroll this switchs it to the pistol.
        if (weaponSwap == rifle.GetComponent<RifleWeapon>())
        {
            pistol.SetActive(true);
            rifle.SetActive(false);
            pc.pistolImage.SetActive(true);
            pc.rifleImage.SetActive(false);

            weapon = pistol.GetComponent<PistolWeapon>();
        }
    }

    // Pickup weapon for enemies
    public void PickUpWeapon(Weapon weaponSwap)
    {
        if (hasRifle)
        {
            // If the weapon is a pistol and you scroll this switchs it to the rifle.
            if (weaponSwap == pistol.GetComponent<PistolWeapon>())
            {
                pistol.SetActive(false);
                rifle.SetActive(true);
                weapon = rifle.GetComponent<RifleWeapon>();
            }
        }
    }

    // This makes the hands move to their designated spots
    public void OnAnimatorIK(int layerIndex)
    {
        if(weapon != null)
        {
            anim.SetLookAtPosition(weapon.transform.position + (5 * weapon.transform.forward));
            anim.SetLookAtWeight(1);

            if(weapon.rightHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }

            if (weapon.leftHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            }
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetLookAtWeight(0);
        }
    }

    // Getting hurt by bullets
    private void OnTriggerEnter(Collider other)
    {
        // Checks to see if it has the tag Bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Makes it lose health
            health.Hurt(1);
            // Destorys the bullet
            Destroy(other.gameObject);
        }
    }

    // Death
    public IEnumerator Die()
    {
        //Ragdoll
        StartRagdoll();

        // Remove Enemy from list
        if (gameObject.CompareTag("Enemy"))
        {
            // Removes from list
            GameManager.Instance.enemies.Remove(gameObject);
            

            // Wait
            yield return new WaitForSeconds(5);
            
            // Drops an Item
            Instantiate(GameManager.Instance.GetComponent<DropManager>().DropItem(), gameObject.transform.position, Quaternion.identity);
            
            // Adds to the kill feed
            GameManager.Instance.enemiesKilled++;

            // Destroy
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Player"))
        {

            // Reset Health
            pc.GetComponent<Health>().currentHealth = pc.GetComponent<Health>().maxHealth;
            
            // Makes the health look like it is at 0
            pc.healthText.text = "Health: 0/" + pc.GetComponent<Health>().maxHealth;

            // wait
            yield return new WaitForSeconds(3);

            // Stop Ragdoll
            StopRagdoll();

            // Remove life
            pc.lives--;

            // Sets isDead to false
            isDead = false;
        }
    }

    // Starts Ragdoll
    public void StartRagdoll()
    {
        // Turn off animator
        anim.enabled = false;

        // Turn on ragdoll colliders
        foreach(Collider currentCollider in ragdollColliders)
        {
            currentCollider.enabled = true;
        }

        // Turn on ragdoll rigidbodies
        foreach(Rigidbody currentRigidbody in ragdollRigidbodies)
        {
            currentRigidbody.isKinematic = false;
        }

        // Turn off big collider
        topCollider.enabled = false;

        // Turn off big Rigidbody
        topRigidbody.isKinematic = true;

        if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<AIController>().enabled = false;
        }

        if (gameObject.CompareTag("Player"))
        {
            GetComponent<PlayerController>().enabled = false;
        }


    }

    // Ends ragdoll
    public void StopRagdoll()
    {
        // Turn on animator
        anim.enabled = true;

        // Turn off ragdoll colliders
        foreach (Collider currentCollider in ragdollColliders)
        {
            currentCollider.enabled = false;
        }

        // Turn off ragdoll rigidbodies
        foreach (Rigidbody currentRigidbody in ragdollRigidbodies)
        {
            currentRigidbody.isKinematic = true;
        }

        // Turn on big collider
        topCollider.enabled = true;

        // Turn on big Rigidbody
        if (gameObject.CompareTag("Player"))
        {
            topRigidbody.isKinematic = false;
            GetComponent<PlayerController>().enabled = true;
        }
        
    }
}
