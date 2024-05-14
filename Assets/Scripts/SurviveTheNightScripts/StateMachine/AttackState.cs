using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * When a character switches to the Attack State, it goes to the player and attack them.
 */

public class AttackState : State
{
    public Enemy enemy;
    public DeathState deathState;
    public Player playerHealth;
    public float cooldownTime = 3f;
    private float lastUsedTime;
    private void Awake()
    {
    }

    public override State RunCurrentState()
    {
        navMeshAgent.isStopped= false;
        if (enemy != null)
        {
            //if dead play death animation and disappear.death state
            if (enemy.isDead)
            {
                ResetAnimationTrigger("Gallop");
                ResetAnimationTrigger("Idle");
                ResetAnimationTrigger("Attack");
                SetAnimationTrigger("Idle");
                return deathState;
            }
            else {
                if (Time.time > lastUsedTime + cooldownTime) {
                    lastUsedTime = Time.time;

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(enemy.damage);
                        if (enemy.attackSound) {
                            enemy.attackSound.Play();
                        }
                    }
                    else
                    {
                        Debug.LogError("player script is missing from the player gameobject in XR origin");
                    }
                }

            }
        }
        else {
            Debug.LogError("Enemy script is missing");
        }

        //if (nearPlayer.isEnemyNear)
        if(enemy.isNearPlayer)
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
