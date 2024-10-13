using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Data/DialogSO")]
public class DialogSO : ScriptableObject
{
    [field: TextArea]
    [SerializeField] private string[] dialogs;

    public string[] GetDialogs() {  return dialogs; }
}
