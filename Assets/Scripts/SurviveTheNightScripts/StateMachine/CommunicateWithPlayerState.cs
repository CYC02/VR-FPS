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
    public AlertState alertState;
    public GetResourcesState getResourceState;

    // The time it takes for the player to not look at Bobby before switches back to the idle state
    [SerializeField] private float noLookDuration = 3f;
    
    // Timer to keep track how long the player is not looking at Bobby 
    private float timer = 0;
    // Bool to see if the timer is not looking at Bobby had started or not
    private bool startedTimer = false;

    [SerializeField] private GameObject bobbyUI;

    private bool playerClickGetResourcesButton = false;

    public override State RunCurrentState()
    {
        bobbyUI.SetActive(true);

        // Bobby switches states when the player clicks the Get Resources Button
        if (playerClickGetResourcesButton) {
            playerClickGetResourcesButton = false;
            SetAnimationTrigger("RetractLeftHand");
            startedTimer = false;
            timer = 0f;
            bobbyUI.SetActive(false);
            return getResourceState;
        }

        //start timer to see if player is still hovering Bobby
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
                    bobbyUI.SetActive(false);
                    return alertState;
                }
            }
        }
        else {
            // looking at Bobby
            SetAnimationTrigger("ExtendLeftHand");

        }
        //SetAnimationTrigger("RetractLeftHand");
        return this;

    }

    public void OnClickGetResources() {
        playerClickGetResourcesButton = true;
    }
}
