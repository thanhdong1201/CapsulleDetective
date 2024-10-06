using UnityEngine;

public class AttackState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }
    public void Enter(AiAgent agent)
    {
        agent.Enemy.EnterAttack();
    }
    public void Update(AiAgent agent)
    {
        if (agent.IsTargetDead())
        {
            agent.StateMachine.ChangeState(AiStateId.Patrol);
            return;
        }

        agent.transform.LookAt(agent.Target);
        agent.NavMeshAgent.destination = agent.transform.position;

        agent.UpdateDistance();
        if (!agent.CanAttack()) agent.StateMachine.ChangeState(AiStateId.Chase);
    }
    public void Exit(AiAgent agent)
    {
        agent.Enemy.ExitAttack();
    }
}
