using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

/*
 * Reference: https://www.youtube.com/watch?v=cnpJtheBLLY
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
    private void Start()
    {
        character = transform.parent.parent.gameObject;
        anim = character.GetComponentInChildren<Animator>();
        fieldView = character.GetComponent<FieldOfView>();
        navMeshAgent = character.GetComponent<NavMeshAgent>();
        characterCollider = character.GetComponent<Collider>();
        nearPlayer = fieldView.player.GetComponentInChildren<NearPlayer>();
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

}
