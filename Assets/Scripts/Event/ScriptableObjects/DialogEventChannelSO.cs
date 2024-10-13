using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/DialogType Event Channel")]
public class DialogEventChannelSO : DescriptionBaseSO
{
    public UnityAction<DialogSO> OnEventRaised;

    public void RaiseEvent(DialogSO dialogSO)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(dialogSO);
    }
}
