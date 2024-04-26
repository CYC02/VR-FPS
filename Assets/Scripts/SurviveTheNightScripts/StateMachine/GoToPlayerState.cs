using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This state is only for friendly agents.
 * When an agent is in this state, the agent sees the player and stands close to them.
 */

public class GoToPlayerState : State
{
    public IdleState idleState;

    public override State RunCurrentState()
    {

        if (nearPlayer.isBobbyNear)
        {
            //when the friendly agent is close enough, they stop near player
            navMeshAgent.isStopped = true;
            character.transform.LookAt(fieldView.player.transform);
            ResetAnimationTrigger("Run");
            return idleState;
        }
        else {
            //friendly agent runs towards the player and stands near them
            SetAnimationTrigger("Run");
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = fieldView.player.transform.position;
            return this;
        }


    }

}
