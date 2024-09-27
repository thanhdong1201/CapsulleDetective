using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{ 
    public enum SoundFX
    {
        MoveCursor,
        ClickButton,
        PickUp,
        GetQuest,
        CorrectItem, 
        WrongItem, 
        OpenInventory,
        CloseInventory,
        PauseMenu,
    }
    private static GameObject audioSourceGameObject;
    private static AudioSource audioSource;
    private static AudioMixerGroup audioMixerGroup;
    private static AudioClipSO audioClipSO;
    public static void GetAudioClipSO(AudioClipSO audioClipData)
    {
        audioClipSO = audioClipData;

        if (audioSourceGameObject == null)
        {
            audioSourceGameObject = new GameObject("AudioSource");
            audioSourceGameObject.AddComponent<AudioSource>();
            audioSource = audioSourceGameObject.GetComponent<AudioSource>();
            audioMixerGroup = audioClipSO.AudioMixerGroup;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }
    public static void PlaySound(SoundFX soundFX)
    {
        audioSource.PlayOneShot(GetAudioClip(soundFX));
    }
    private static AudioClip GetAudioClip(SoundFX soundFX)
    {
        AudioClip audioClip;

        switch (soundFX)
        {
            default:
                //return true;
            case SoundFX.MoveCursor:
                audioClip = audioClipSO.MoveCursor;
                break;
            case SoundFX.ClickButton:
                audioClip = audioClipSO.ClickButton;
                break;
            case SoundFX.PickUp:
                audioClip = audioClipSO.PickUp;
                break;
            case SoundFX.GetQuest:
                audioClip = audioClipSO.GetQuest;
                break;
            case SoundFX.CorrectItem:
                audioClip = audioClipSO.CorrectItem;
                break;
            case SoundFX.WrongItem:
                audioClip = audioClipSO.WrongItem;
                break;
            case SoundFX.OpenInventory:
                audioClip = audioClipSO.OpenInventory;
                break;
            case SoundFX.CloseInventory:
                audioClip = audioClipSO.CloseInventory;
                break;
            case SoundFX.PauseMenu:
                audioClip = audioClipSO.PauseMenu;
                break;
        }
        return audioClip;
    }
}
