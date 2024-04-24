using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This state is only for friendly agents.
 * When an agent is in this state, the agent sees the player and stands close to them.
 */

public class StayNearPlayerState : State
{

    public override State RunCurrentState()
    {
        //anim.SetTrigger("Run");
        return this;
    }
}
