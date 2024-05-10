using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State
{
    public IdleState idleState;
    public CommunicateWithPlayerState commState;
    private float timer = 0f;
    private bool startedTimer = false;
    [SerializeField] private float turnTime = 5f;

    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        
        startedTimer = true;
        timer += Time.deltaTime;

        if (timer >= turnTime) {
            //reset timer
            timer = 0f;

            //bobby rotates
            character.transform.Rotate(Vector3.up, 45f);
        }

        if (nearPlayer.isBobbyNear && bobbyGaze.isHovered) {
            ResetAnimationTrigger("Idle");
            character.transform.LookAt(fieldView.player.transform);
            return commState;
        }

        return this;
    }
}
