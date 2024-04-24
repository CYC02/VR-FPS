using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Reference: https://www.youtube.com/watch?v=cnpJtheBLLY
 * Author: Cindy Chan
 * This is handles how states are switched in the state machine.
 */

public class StateManager : MonoBehaviour
{
    public State currentState;

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine() {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);

        }

    }

    private void SwitchToTheNextState(State nextState) {
        currentState = nextState;
    }
}
