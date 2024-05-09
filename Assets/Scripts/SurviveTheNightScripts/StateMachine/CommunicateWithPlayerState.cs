using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This is a state that only Bobby can switched to.
 * Only switched to this state when Bobby is near the player and the player's gaze triggers the hover.
 * Bobby extends his arm and the player can interact with the sockets in his hands.
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
                    // after not looking at Bobby, go back to idle state
                    SetAnimationTrigger("RetractLeftHand");
                    startedTimer= false;
                    timer = 0f;
                    return idleState;
                }
            }
        }
        else {
            // looking at Bobby
            SetAnimationTrigger("ExtendLeftHand");

        }
        SetAnimationTrigger("RetractLeftHand");
        return this;

    }
}
