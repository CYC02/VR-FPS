using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * When a character switches to the Attack State, its behavior
 * depends on whether who the character is. (Bobby or enemy animal)
 */

public class AttackState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
