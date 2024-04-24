using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/*
 * Reference: https://www.youtube.com/watch?v=cnpJtheBLLY
 * Author: Cindy Chan
 * This is the abstract class for all States.
 * States are managed by the StateManager.
 */
public abstract class State : MonoBehaviour
{
    protected GameObject character;
    protected Animator anim;
    protected bool startIdleAnimation;

    private void Start()
    {
        character = transform.parent.parent.gameObject;
        anim = character.GetComponentInChildren<Animator>();
    }

    public abstract State RunCurrentState();


    //Set Animation Trigger
    protected void SetAnimationTrigger(string trigger)
    {
        anim.ResetTrigger(trigger);
        startIdleAnimation = false;
    }

    //Reset Animation Trigger
    protected void ResetAnimationTrigger(string trigger)
    {
        anim.ResetTrigger(trigger);
        startIdleAnimation = false;
    }

}
