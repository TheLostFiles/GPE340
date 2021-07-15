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
    private List<Collider> ragdollColliders;
    private List<Rigidbody> ragdollRigidbodies;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        topCollider = GetComponent<Collider>();
        topRigidbody = GetComponent<Rigidbody>();

        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        StopRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if health is zero
        if(health.currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

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
                weapon = rifle.GetComponent<RifleWeapon>();
            }
        }
        // If the weapon is a rifle and you scroll this switchs it to the pistol.
        if (weaponSwap == rifle.GetComponent<RifleWeapon>())
        {
            pistol.SetActive(true);
            rifle.SetActive(false);
            weapon = pistol.GetComponent<PistolWeapon>();
        }
    }

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

    // This makes the hands move to their designated spots.
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

    public IEnumerator Die()
    {
        //Ragdoll
        StartRagdoll();

        // Remove Enemy from list
        if (gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.enemies.Remove(gameObject);
        }
        
        // Wait
        yield return new WaitForSeconds(5);

        // Destroy
        Destroy(gameObject);
    }

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
        
        
    }

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
        }
        
    }
}
