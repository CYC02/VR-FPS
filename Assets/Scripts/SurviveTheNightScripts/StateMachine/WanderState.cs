using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * References:
 * https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
 * https://medium.com/@victormct/creating-a-cooldown-system-in-unity-d250ca87b487
 * 
 * Author: Cindy Chan
 * This state is for all characters.
 * The character will go to a nearby avaliable destination and look around.
 */

public class WanderState : State
{
    public GoToPlayerState goToPlayerState;
    public float cooldownTime = 3f;
    private float lastUsedTime;
    public override State RunCurrentState()
    {
        SetAnimationTrigger("Walk");
        if (!fieldView.canSeePlayer)
        {
            if (Time.time > lastUsedTime + cooldownTime) { 
                lastUsedTime= Time.time;

                //wander
                //go to a random point within radius
                float range = fieldView.radius;
                Vector3 point;

                if (RandomPoint(character.transform.position, range, out point)) {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    navMeshAgent.destination = point;
                }
            }

            if (character.transform.position == navMeshAgent.destination)
            {
                ResetAnimationTrigger("Walk");
                SetAnimationTrigger("Idle");
            }
            else {
                ResetAnimationTrigger("Idle");
                SetAnimationTrigger("Walk");
            }
            return this;

        }
        else {
            //can see player and go to the player
            if (character.CompareTag("Friendly")) {
                if (character.layer == LayerMask.NameToLayer("Bobby")) {
                    ResetAnimationTrigger("Walk");
                    return goToPlayerState;
                }
            }
        }
        return this;
    }

    //find random point on nav mesh surface
    //returns a bool of whether a random is found
    //center is the center point of the random search area
    //range is how big the random search area is
    //if a random point is found, it will be stored in result
    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        for (int i = 0; i< 30; i++) {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
