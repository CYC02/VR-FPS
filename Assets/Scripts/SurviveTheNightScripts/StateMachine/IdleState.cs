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

    public StayNearPlayerState nearPlayerState;
    public AttackState attackState;

    public bool canSeeThePlayer;

    public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            if (character.CompareTag("Friendly"))
            {
                ResetAnimationTrigger("Idle");
                return nearPlayerState;
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

        // Play Idle Animation
        if (!startIdleAnimation) {
            if (anim != null) {
                SetAnimationTrigger("Idle");
            }
            else {
                Debug.LogWarning("Bobby's first child should have the Animator component!");
            }
        }

        return this;
    }
}
