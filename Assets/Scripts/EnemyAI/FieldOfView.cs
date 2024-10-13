using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public TransformTargetSO targetTransform;
    public Transform target {  get; private set; }
    public Transform transformPosition;
    public float radius;
    [Range(0,360)] public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool CanSeePlayer {  get; private set; }

    private void Start()
    {
        //target = targetTransform.GetTargetTransform();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        FieldOfViewCheck();
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transformPosition.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transformPosition.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transformPosition.position, target.position);

                if (!Physics.Raycast(transformPosition.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }       
            }
            if (Vector3.Angle(transform.forward, directionToTarget) >= angle / 2)
            {
                CanSeePlayer = false;
            }       
        }
        //else if (CanSeePlayer)
        //{
        //    CanSeePlayer = false;
        //}
    }
}

