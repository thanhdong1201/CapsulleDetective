using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] waypoints;

    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;

    public bool isActive;
    private float m_WaitTime;

    private void Awake()
    {
        
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        capsuleCollider.isTrigger = false;
    }
    private void Update()
    {
        if (isActive)
        {
            Patrol();
        }
        else
        {
            Stop();
        }
    }
    void Walk()
    {
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.isStopped = false;
    }
    void Stop()
    {
        capsuleCollider.isTrigger = false;
        navMeshAgent.speed = 0;
        navMeshAgent.isStopped = true;
    }
    void Patrol()
    {     
        capsuleCollider.isTrigger = true;
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0)
            {
                Walk();
                navMeshAgent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].position);
                m_WaitTime = 0.2f;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
        else
        {
            Walk();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && isActive)
    //    {
    //        other.GetComponent<GameOver>().PlayDieVisual();
    //    }
    //}
}
