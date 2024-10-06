using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "Data/ConfigAgent")]
public class AiAgentConfigSO : ScriptableObject
{
    public float walkSpeed = 1.8f;
    public float runSpeed = 5.0f;

    public float maxTime = 1.0f;
    public float maxIdleTime = 2.0f;
    public float maxChaseTime = 3.0f;
    public float maxDistance = 1.0f;
    public float maxSightDistance = 5.0f;
}
