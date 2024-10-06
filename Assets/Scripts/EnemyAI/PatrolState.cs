using UnityEngine;

public class PatrolState : AiState
{
    private int index = 0;
    private float timer = 1.0f;
    public AiStateId GetId()
    {
        return AiStateId.Patrol;
    }
    public void Enter(AiAgent agent)
    {
        agent.NavMeshAgent.speed = agent.Config.walkSpeed;
        agent.Enemy.NoTarget();
    }
    public void Update(AiAgent agent)
    {
        agent.transform.LookAt(agent.NavMeshAgent.destination);

        if (agent.HasReachedDestination())
        {
            index ++;
            if (index >= agent.PatrolPoints.Length) index = 0;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {      
                agent.NavMeshAgent.destination = agent.PatrolPoints[index].position;
                timer = Random.Range(1, 1.5f);
            }
        }

        if (agent.IsTargetDead()) return;

        if (agent.FieldOfView.CanSeePlayer) agent.StateMachine.ChangeState(AiStateId.Chase);
    }
    public void Exit(AiAgent agent)
    {
        
    }
}
