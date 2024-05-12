using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResourcesState : State
{
    public override State RunCurrentState()
    {
        ResetAnimationTrigger("RetractLeftHand");
        ResetAnimationTrigger("ExtendLeftHand");
        return this;
    }
}
