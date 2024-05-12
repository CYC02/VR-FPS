using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This states is for the character to wander and look for Resources to store in their storage;
 * Similar to the wander state.
 */
public class GetResourcesState : State
{
    public float cooldownTime = 3f;
    private float lastUsedTime;
    public BobbyStorage bobbyStorage;
    public override State RunCurrentState()
    {
        ResetAnimationTrigger("RetractLeftHand");
        ResetAnimationTrigger("ExtendLeftHand");
        SetAnimationTrigger("Walk");
        navMeshAgent.isStopped= false;
        if (!fieldView.canSeeTarget)
        {
            if (Time.time > lastUsedTime + cooldownTime)
            {
                lastUsedTime = Time.time;

                //wander
                float range = fieldView.radius;
                Vector3 point;

                if (RandomPoint(character.transform.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.green, 1.0f);
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = point;
                }
            }

            if (character.transform.position == navMeshAgent.destination)
            {
                ResetAnimationTrigger("Walk");
                SetAnimationTrigger("Idle");
            }
            else
            {
                ResetAnimationTrigger("Idle");
                SetAnimationTrigger("Walk");
            }
        }
        else {
            //can see target and go to target.
            if (character.CompareTag("Friendly"))
            {
                if (character.layer == LayerMask.NameToLayer("Bobby"))
                {
                    //ResetAnimationTrigger("Walk");
                    //collect resource into storage
                }
            }
        }

        return this;
    }
}
