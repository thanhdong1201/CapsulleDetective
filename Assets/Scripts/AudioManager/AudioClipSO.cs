using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioClipsData", menuName = "Data/AudioClipData")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip MoveCursor, ClickButton, PickUp, GetQuest, CorrectItem, WrongItem, OpenInventory, CloseInventory, PauseMenu;
    public AudioMixerGroup AudioMixerGroup;
}
