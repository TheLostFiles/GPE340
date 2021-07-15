using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    public Pawn pawn;

    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for the player to press down shift making the character sprint.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pawn.speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pawn.speed /= 2;
        }

        RotateToMousePointer();

        //Gets the movement of a joystick so that the charcter can move.
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Claps down the speed so keyboard doesn't move faster than joystick in diaginal directions.
        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);
        //Makes it so the player will always go in one direction with W instead of following the direction of the player.
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        //These set the animations for the speed.
        pawn.anim.SetFloat("Forward", animationDirection.z * pawn.speed);
        pawn.anim.SetFloat("Right", animationDirection.x * pawn.speed);


        //Handle Trigger 
        if (Input.GetButtonDown("Fire1"))
        {
            pawn.weapon.OnMainActionStart.Invoke();
        }
        if (Input.GetButton("Fire1"))
        {
            pawn.weapon.OnMainActionHold.Invoke();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pawn.weapon.OnTriggerRelease();
            pawn.weapon.OnMainActionEnd.Invoke();
        }

        // Checks the scroll wheel scrolls.
        if (Input.mouseScrollDelta.y != 0)
        {
            // Runs EquipWeapon.
            pawn.EquipWeapon(pawn.weapon);
        }
        // Keeps the health UI Updated.
        healthText.text = "Health: " + GetComponent<Health>().currentHealth + "/" + GetComponent<Health>().maxHealth;
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, pawn.turnSpeed * Time.deltaTime);
    }

    public void AddToScore(float pointsToAdd)
    {
        // TODO: Add points to my score
    }
}
