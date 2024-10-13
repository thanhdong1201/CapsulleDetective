using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public string Description;
    public Sprite Sprite;
    //public Transform Prefab;
    public GameObject Prefab;
}
