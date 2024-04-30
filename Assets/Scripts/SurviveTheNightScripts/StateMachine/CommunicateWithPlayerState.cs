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

    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        //player gaze is true and switched to this state
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
        return this;

    }
}
