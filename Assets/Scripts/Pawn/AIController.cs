using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform followTarget;

    public float decisionDelay;
    private float nextDecisionTime;

    public Pawn pawn;


    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        nextDecisionTime = Time.time;
        followTarget = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Makes sure he can shoot with both gun types
        if(pawn.weapon == pawn.pistol.GetComponent<PistolWeapon>())
        {
            pawn.weapon.OnMainActionStart.Invoke();
        }

        if (pawn.weapon == pawn.rifle.GetComponent<RifleWeapon>())
        {
            pawn.weapon.OnMainActionHold.Invoke();
        }

        pawn.PickUpWeapon(pawn.weapon);

        // If it is time to update.
        if (Time.time >= nextDecisionTime)
        {
            // Update.
            agent.SetDestination(followTarget.position);
            // Save our next decision time.
            nextDecisionTime = Time.time + decisionDelay;
        }
    }

    private void FixedUpdate()
    {
        // Get the desired movement from the agent.
        Vector3 desiredMovement = agent.desiredVelocity;
        // Invert it so it works h root motion.
        desiredMovement = this.transform.InverseTransformDirection(desiredMovement);
        //normalize
        desiredMovement = desiredMovement.normalized;
        // add speed
        desiredMovement *= pawn.speed;

        // Pass it into our root motion animator.
        pawn.anim.SetFloat("Forward", desiredMovement.z);
        pawn.anim.SetFloat("Right", desiredMovement.x);
    }

    private void OnAnimatorMove()
    {
        // When the animator moves the cahracter tell navmesh we moved and run calculations.
        agent.velocity = pawn.anim.velocity;
    }

    
}
