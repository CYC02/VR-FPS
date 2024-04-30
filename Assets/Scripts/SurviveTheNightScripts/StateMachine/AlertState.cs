using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State
{
    public IdleState idleState;
    public override State RunCurrentState()
    {
        SetAnimationTrigger("Idle");
        return this;
    }
}
