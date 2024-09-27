using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;

    [Header("AudioMixerGroup")]
    [SerializeField] private AudioMixerGroup masterMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectMixerGroup;

    [Header("AudioClips")]
    public AudioClipSO AudioClipSO;

    [Header("Save")]
    [SerializeField] private JsonSaveSystem jsonSaveSystem;

    private float masterVolume;
    private float musicVolume;
    private float soundEffectVolume;

    private void Start()
    {
        PrepareData();
        SoundManager.GetAudioClipSO(AudioClipSO);
    }
    private void PrepareData()
    {
        jsonSaveSystem.LoadFromJson();
        masterVolume = jsonSaveSystem.Data.MasterVolume;
        musicVolume = jsonSaveSystem.Data.MusicVolume;
        soundEffectVolume = jsonSaveSystem.Data.SoundEffectVolume;
        UpdateValue();
    }
    private void Update()
    {
        UpdateValue();
    }
    private void UpdateValue()
    {
        masterSlider.value = masterVolume;
        masterMixerGroup.audioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);

        musicSlider.value = musicVolume;
        musicMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);

        soundEffectSlider.value = soundEffectVolume;
        soundEffectMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(soundEffectVolume) * 20);
    }
    public void OnChangeMasterSlider(float value)
    {
        masterVolume = value;
        jsonSaveSystem.Data.MasterVolume = value;
        jsonSaveSystem.SaveToJson();
    }
    public void OnChangeMusicSlider(float value)
    {
        musicVolume = value;
        jsonSaveSystem.Data.MusicVolume = value;
        jsonSaveSystem.SaveToJson();
    }
    public void OnChangeSoundEffectSlider(float value)
    {
        soundEffectVolume = value;
        jsonSaveSystem.Data.SoundEffectVolume = value;
        jsonSaveSystem.SaveToJson();
    }
}
