using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public override State RunCurrentState()
    {
        SetAnimationTrigger("Death");
        return this;
    }
}
