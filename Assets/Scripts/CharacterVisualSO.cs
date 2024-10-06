using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Visual", menuName = "Data/CharacterVisualSO")]
public class CharacterVisualSO : ScriptableObject
{
    [SerializeField] private Material takeDamageMaterial;
    [SerializeField] private Material dieMaterial;
    [SerializeField] private Transform effect;

    public Material TakeDamageMaterial => takeDamageMaterial;
    public Material DieMaterial => dieMaterial;
    public Transform Effect => effect;  
}
