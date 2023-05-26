using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public ChaseState chaseState;
    public bool patrols;

    private GuardAI guardAI;

    public void Start() {
        patrols = gameObject.transform.parent.gameObject.transform.parent.GetComponent<Guard>().patrols;

        // Get the guardAI component
        guardAI = GetComponent<GuardAI>();

        if(guardAI.reachedEndOfPath) {
            guardAI.target = guardAI.initialPos;
        }
    }

    public override State RunCurrentState()
    {
        if(!patrols) {
            return chaseState;
        }
        return this;
    }
}
