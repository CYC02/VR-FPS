using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AlertState : State
{
    public CommunicateWithPlayerState commState;
    
    //Manage the time when Bobby rotates to look around
    private float turnTimer = 0f;
    //Time it takes before Bobby rotates
    [SerializeField] private float turnTime = 5f;
    
    //Bool keep track of how long the player looks at Bobby before he turns around
    private bool isPlayerLooking = false;
    private float playerLookingTimer = 0f;
    [SerializeField] private float playerLookingDuration = 5f;
    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        
        turnTimer += Time.deltaTime;

        if (turnTimer >= turnTime) {
            //reset timer
            turnTimer = 0f;

            //bobby rotates
            character.transform.Rotate(Vector3.up, 45f);
        }

        //reset and start the timer that keeps track of whether the player starts looking at Bobby
        if (bobbyGaze.isHovered == true && isPlayerLooking == false) {
            playerLookingTimer = 0f;
            isPlayerLooking= true;
        }

        //checking if the player is continuous looking at Bobby for a certain duration of time
        if (isPlayerLooking) {
            if (bobbyGaze.isHovered) {
                playerLookingTimer += Time.deltaTime;
                if (playerLookingTimer >= playerLookingDuration) {
                    //player looks at Bobby for a certain amount of time
                    //go to comm state
                    if (nearPlayer.isBobbyNear && bobbyGaze.isHovered) {
                        ResetAnimationTrigger("Idle");
                        playerLookingTimer = 0f;
                        character.transform.LookAt(fieldView.player.transform);
                        return commState;
                    }
                }
            }
            //player stopped looking a Bobby before the timer reaches the playerLookingDuration value
            else{
                isPlayerLooking= false;
            }
        }



        return this;
    }
}
