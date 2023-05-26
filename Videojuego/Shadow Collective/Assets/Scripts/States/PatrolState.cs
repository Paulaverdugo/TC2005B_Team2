using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public ChaseState chaseState;
    public bool isAlerted;
    GameObject guard;

    private GuardAI guardAI;

    public void Start() {
        guard = gameObject.transform.parent.gameObject;
        isAlerted = guard.GetComponent<Guard>().isAlerted;
        // Get the guardAI component
        guardAI = GetComponent<GuardAI>();
    }

    public override State RunCurrentState()
    {
        if(isAlerted) {
            return chaseState;
        }
        return this;
    }
}
