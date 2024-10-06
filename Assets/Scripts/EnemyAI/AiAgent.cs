using System;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine StateMachine;
    public AiStateId InitialState;
    public NavMeshAgent NavMeshAgent;
    public AiAgentConfigSO Config;
    public FieldOfView FieldOfView;
    public Animator Animator;
    public Enemy Enemy;
    public Transform Target { get; private set; }
    public Transform[] PatrolPoints;

    private float distance;
    private int index;
    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new AiStateMachine(this);
        StateMachine.RegisterState(new IdleState());
        StateMachine.RegisterState(new PatrolState());
        StateMachine.RegisterState(new ChaseState());
        StateMachine.RegisterState(new AttackState());
        StateMachine.ChangeState(InitialState);

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        Target = go.transform;
    }
    private void Update()
    {
        StateMachine.Update();
        Debug.Log("State: " + StateMachine.currentState);
    }
    public bool IsTargetDead()
    {
        return Target.GetComponent<Health>().IsPlayerDead;
    }
    public bool CanAttack()
    {    
        return distance <= NavMeshAgent.stoppingDistance;
    }
    public bool HasReachedDestination()
    {
        return NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance + 0.2f;
    }
    public void UpdateDistance()
    {
        distance = (Target.position - NavMeshAgent.transform.position).magnitude;
    }
}
