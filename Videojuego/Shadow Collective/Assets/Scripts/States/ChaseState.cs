using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public PatrolState patrolState;
    public bool patrols;

    private GuardAI guardAI;

    public void Start() {
        patrols = gameObject.transform.parent.gameObject.GetComponent<Guard>().patrols;

        // Get the guardAI component
        guardAI = GetComponent<GuardAI>();
    }

    public override State RunCurrentState()
    {
        if (patrols) {
            return patrolState;
        }
        return this;
    }
}
