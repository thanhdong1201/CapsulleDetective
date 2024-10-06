using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AiState
{
    private float timer;
    private float seeTargetTimer;
    public AiStateId GetId()
    {
        return AiStateId.Chase;
    }
    public void Enter(AiAgent agent)
    {
        agent.NavMeshAgent.speed = agent.Config.runSpeed;
        timer = agent.Config.maxTime;
        seeTargetTimer = agent.Config.maxChaseTime;
        agent.Enemy.DetectTarget();
    }
    public void Update(AiAgent agent)
    {
        if(!agent.enabled) return;
 
        if(!agent.NavMeshAgent.hasPath) agent.NavMeshAgent.SetDestination(agent.Target.position);

        agent.transform.LookAt(agent.Target);

        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            Vector3 direction = (agent.Target.position - agent.NavMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.Config.maxDistance * agent.Config.maxDistance)
            {
                if (agent.NavMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.NavMeshAgent.SetDestination(agent.Target.position);
                }
            }
            timer = agent.Config.maxTime;
        }

        if (agent.FieldOfView.CanSeePlayer) seeTargetTimer = agent.Config.maxChaseTime;
        if (!agent.FieldOfView.CanSeePlayer)
        {
            seeTargetTimer -= Time.deltaTime;
            if (seeTargetTimer <= 0.0f) agent.StateMachine.ChangeState(AiStateId.Patrol);
        }

        agent.UpdateDistance();

        if (agent.CanAttack()) agent.StateMachine.ChangeState(AiStateId.Attack);
    }
    public void Exit(AiAgent agent)
    {
        
    }
}
