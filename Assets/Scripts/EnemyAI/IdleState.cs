using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AiState
{
    private float timer;

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.NavMeshAgent.ResetPath();
        Random.Range(1, 1.5f);
    }
    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(1, 1.5f);
            agent.StateMachine.ChangeState(AiStateId.Patrol);
        }

        if (agent.IsTargetDead()) return;

        if (agent.FieldOfView.CanSeePlayer) agent.StateMachine.ChangeState(AiStateId.Chase);
    }
    public void Exit(AiAgent agent)
    {

    }
}
