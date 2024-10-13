using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Data/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public ItemSO FirstRequireItem;
    public ItemSO SecondRequireItem;
    public ItemSO FinalItem;
}
