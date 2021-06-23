using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
    private Animator anim;

    public float speed;
    public float turnSpeed = 180;
    public Camera playerCamera;

    public Weapon weapon;

    public GameObject pistol;
    public GameObject rifle;
    public bool hasRifle;

    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for the player to press down shift making the character sprint.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }

        //Gets the movement of a joystick so that the charcter can move.
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Claps down the speed so keyboard doesn't move faster than joystick in diaginal directions.
        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);
        //Makes it so the player will always go in one direction with W instead of following the direction of the player.
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        //These set the animations for the speed.
        anim.SetFloat("Forward", animationDirection.z * speed);
        anim.SetFloat("Right", animationDirection.x * speed);

        //Calls function.
        RotateToMousePointer();

        //Handle Trigger 
        if (Input.GetButtonDown("Fire1"))
        {
            
            weapon.OnMainActionStart.Invoke();
        }
        if(Input.GetButton("Fire1"))
        {
            
            weapon.OnMainActionHold.Invoke();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.OnTriggerRelease();
            weapon.OnMainActionEnd.Invoke();
        }

        // Checks the scroll wheel scrolls.
        if(Input.mouseScrollDelta.y != 0)
        {
            // Runs EquipWeapon.
            EquipWeapon(weapon);
        }
        // Keeps the health UI Updated.
        healthText.text = "Health: " + GetComponent<Health>().currentHealth + "/" + GetComponent<Health>().maxHealth;

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

    public void RotateToMousePointer()
    {
        //Makes a plane.
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        //Gets the screen point where the mouse is and sets that to the ray.
        Ray theRay = playerCamera.ScreenPointToRay(Input.mousePosition);

        //Float.
        float distance;

        //Makes the raycast and pulls out dstance from it.
        groundPlane.Raycast(theRay, out distance);
        //Makes targetpoint equal to distance.
        Vector3 targetPoint = theRay.GetPoint(distance);

        //makes it turn.
        RotateTorwards(targetPoint);
    }

    public void RotateTorwards(Vector3 lookAtPoint)
    {
        //Quaternion.
        Quaternion goalRotation;

        //Give the quaternion something to turn to.
        goalRotation = Quaternion.LookRotation(lookAtPoint - transform.position, Vector3.up);

        //Sets a speed and place to turn to.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
    }

    public void AddToScore(float pointsToAdd)
    {
        // TODO: Add points to my score
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
}
