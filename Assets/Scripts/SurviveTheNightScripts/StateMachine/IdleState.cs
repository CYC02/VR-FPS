using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This is the Idle state for the character.
 * During the Idle State, they will play the Idle animation.
 * There are different states that can be switched to depending on the character's tag (Friendly, Enemy, etc...)
 */

public class IdleState : State
{

    public GoToPlayerState goToPlayerState;
    public AttackState attackState;

    public override State RunCurrentState()
    {   

        if (fieldView.canSeePlayer)
        {
            if (character.CompareTag("Friendly"))
            {
                if (character.layer == LayerMask.NameToLayer("Bobby")) {
                    if (nearPlayer.isBobbyNear) {
                        SetAnimationTrigger("Idle");
                        return this;
                    }
                    else{
                        ResetAnimationTrigger("Idle");
                        return goToPlayerState;
                    }
                }
                else{

                    return this;
                }
                
            }
            else if (character.CompareTag("Enemy"))
            {
                ResetAnimationTrigger("Idle");
                return attackState;
            }
            else {
                return this;
            }
            
        }



        return this;
    }
}
