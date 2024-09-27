
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSetting")]
public class AudioSettingSO : ScriptableObject
{
    [SerializeField] private float _currentMasterVolumeValue;
    [SerializeField] private float _currentMusicVolumeValue;
    [SerializeField] private float _currentSoundEffectVolumeValue;

    public float CurrentMasterVolumeValue => _currentMasterVolumeValue;
    public float CurrentMusicVolumeValue => _currentMusicVolumeValue;
    public float CurrentSoundEffectVolumeValue => _currentSoundEffectVolumeValue;

    public void SetCurrentMasterVolumeValue(float newValue)
    {
        _currentMasterVolumeValue = newValue;
    }
    public void SetCurrentMusicVolumeValue(float newValue)
    {
        _currentMusicVolumeValue = newValue;
    }
    public void SetCurrentSoundEffectVolumeValue(float newValue)
    {
        _currentSoundEffectVolumeValue = newValue;
    }
}
