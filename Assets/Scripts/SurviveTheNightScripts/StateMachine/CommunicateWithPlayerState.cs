using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This is a state that only Bobby can switched to.
 * Only switched to this state when Bobby is near the player and the player's gaze triggers the hover
 */

public class CommunicateWithPlayerState : State
{
    public IdleState idleState;


    [SerializeField] private float noLookDuration = 3f;
    private float timer = 0;
    private bool startedTimer = false;

    public override State RunCurrentState()
    {

        if (bobbyGaze.isHovered == false && startedTimer == false) {
            timer = 0;
            startedTimer = true;
        }
        if (startedTimer)
        {
            if (bobbyGaze.isHovered)
            {
                startedTimer = false;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= noLookDuration)
                {
                    
                    SetAnimationTrigger("RetractLeftHand");
                    startedTimer= false;
                    timer = 0f;
                    return idleState;
                }
            }
        }
        else {
            SetAnimationTrigger("ExtendLeftHand");
        }

        return this;
        //player gaze is true and switched to this state
        /*
        if (bobbyGaze != null)
        {

            if (bobbyGaze.isHovered) {
                ResetAnimationTrigger("RetractLeftHand");
                SetAnimationTrigger("ExtendLeftHand");
            }
            else
            {
                ResetAnimationTrigger("ExtendLeftHand");
                SetAnimationTrigger("RetractLeftHand");
            }
        }
        */
        return this;

    }
}
