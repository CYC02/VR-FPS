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

    public float hoverDuration = 5f;
    public float notHoverDuration = 3f;
    private float hoverTimer = 0f;
    private float notHoverTimer = 0f;
    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        //player gaze is true and switched to this state
        Debug.Log(hoverTimer);
        if (bobbyGaze != null) {
            if (bobbyGaze.isHovered)
            {
                //Bobby observes what the player is trying to do while looking at him
                //character.transform.LookAt(fieldView.player.transform);

                //if the player is holding an object that Bobby can take, then if the player holds it for 5 sec
                //bobby extends his arm to player
                /*
                if (Time.time > holdWaitTime + holdWaitTime) {
                    lastHoldTime= Time.time;

                    //player is holding flashlight
                    ResetAnimationTrigger("Idle");
                    SetAnimationTrigger("ExtendLeftHand");

                }
                */
                notHoverTimer= 0f;
                hoverTimer += Time.deltaTime;
                if (hoverTimer >= hoverDuration) {
                    ResetAnimationTrigger("Idle");
                    SetAnimationTrigger("ExtendLeftHand");
                }

            }
            else {
                //after a certain amount of time, the player doesn't look at Bobby,
                //go back to idle state

                /*
                if (Time.time > lastNoLookWaitTime + noLookWaitTime) {
                    lastNoLookWaitTime = Time.time;
                    ResetAnimationTrigger("ExtendLeftHand");
                    SetAnimationTrigger("RetractLeftHand");
                    return idleState;
                }
                */
                hoverTimer = 0f;
                notHoverTimer += Time.deltaTime;
                if (notHoverTimer >= notHoverDuration) {
                    ResetAnimationTrigger("ExtendLeftHand");
                    SetAnimationTrigger("RetractLeftHand");
                }

                
            }
        }
        
        return this;
    }
}
