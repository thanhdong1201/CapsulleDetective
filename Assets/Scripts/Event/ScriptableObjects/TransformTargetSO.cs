using UnityEngine;

[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class TransformTargetSO : ScriptableObject
{
    [SerializeField] private Transform targetTransform;

    public void UpdateTargetTransform(Transform value)
    {
        targetTransform = value;
    }
    public Transform GetTargetTransform() { return targetTransform; }
}
