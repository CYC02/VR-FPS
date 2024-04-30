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

    /*
    public float noLookWaitTime = 10f;
    private float lastNoLookWaitTime;

    public float holdWaitTime = 3f;
    private float lastHoldTime;
    */

    public float notHoverDuration = 3f;
    private float notHoverTimer = 0f;

    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        //player gaze is true and switched to this state
        if (bobbyGaze != null)
        {
            notHoverDuration += Time.deltaTime;

            if (bobbyGaze.isHovered == false)
            {
                if (notHoverTimer >= notHoverDuration)
                {
                    //player is not looking at Bobby for a certain amount of time
                    ResetAnimationTrigger("ExtendLeftHand");
                    SetAnimationTrigger("RetractLeftHand");

                    Debug.Log("NOTHOVERING");
                }
            }
            else
            {
                notHoverTimer = 0f;
                ResetAnimationTrigger("Idle");
                SetAnimationTrigger("ExtendLeftHand");
                Debug.Log("HOVERING");
            }

        }
        return this;
    }
}
