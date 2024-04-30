using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

/*
 * References:
 * https://www.youtube.com/watch?v=cnpJtheBLLY
 * https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
 * 
 * Author: Cindy Chan
 * This is the abstract class for all States.
 * States are managed by the StateManager.
 */
public abstract class State : MonoBehaviour
{
    protected GameObject character; //Bobby or animal
    protected Animator anim;
    protected FieldOfView fieldView;
    protected NavMeshAgent navMeshAgent;
    protected Collider characterCollider;
    protected NearPlayer nearPlayer;
    protected InputInfo inputInfo;
    private void Start()
    {
        character = transform.parent.parent.gameObject;
        anim = character.GetComponentInChildren<Animator>();
        fieldView = character.GetComponent<FieldOfView>();
        navMeshAgent = character.GetComponent<NavMeshAgent>();
        characterCollider = character.GetComponent<Collider>();
        nearPlayer = fieldView.player.GetComponentInChildren<NearPlayer>();
        inputInfo = character.GetComponent<InputInfo>();
    }

    public abstract State RunCurrentState();


    //Set Animation Trigger
    protected void SetAnimationTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    //Reset Animation Trigger
    protected void ResetAnimationTrigger(string trigger)
    {
        anim.ResetTrigger(trigger);
    }

    //find random point on nav mesh surface
    //returns a bool of whether a random is found
    //center is the center point of the random search area
    //range is how big the random search area is
    //if a random point is found, it will be stored in result
    protected bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
