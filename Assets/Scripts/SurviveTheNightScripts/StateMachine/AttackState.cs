using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * When a character switches to the Attack State, it goes to the player and attack them.
 */

public class AttackState : State
{
    Enemy enemy;
    public DeathState deathState;
    public Player playerHealth;
    private void Awake()
    {
        enemy= GetComponent<Enemy>();
    }

    public override State RunCurrentState()
    {
        if (enemy != null)
        {
            //if dead play death animation and disappear.death state
            if (enemy.isDead) {
                ResetAnimationTrigger("Gallop");
                ResetAnimationTrigger("Idle");
                ResetAnimationTrigger("Attack");
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(enemy.damage);
                }
                else {
                    Debug.LogError("player script is missing from the player gameobject in XR origin");
                }
                return deathState;
            }
        }
        else {
            Debug.LogError("Enemy script is missing");
        }

        if (nearPlayer.isEnemyNear)
        {
            navMeshAgent.isStopped = true;
            character.transform.LookAt(fieldView.player.transform);
            ResetAnimationTrigger("Gallop");
            SetAnimationTrigger("Idle");
            SetAnimationTrigger("Attack");

        }
        else {
            SetAnimationTrigger("Gallop");
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = fieldView.player.transform.position;
        }
        return this;
    }
}
