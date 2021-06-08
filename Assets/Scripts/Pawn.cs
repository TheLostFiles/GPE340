using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    private Animator anim;

    public float speed;
    public float turnSpeed = 180;
    public Camera playerCamera;


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
}
