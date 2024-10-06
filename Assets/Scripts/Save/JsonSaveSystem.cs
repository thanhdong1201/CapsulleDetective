using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveGameData", menuName = "Data/SaveGame")]
public class JsonSaveSystem : ScriptableObject
{
    public Data Data = new Data();

    public void SaveToJson()
    {
        string saveData = JsonUtility.ToJson(Data);
        string filePath = Application.dataPath + "/SaveData.json";
        //Debug.Log(filePath);
        File.WriteAllText(filePath, saveData);
    }
    public void LoadFromJson()
    {
        string filePath = Application.dataPath + "/SaveData.json";
        string saveData = File.ReadAllText(filePath);
        Data = JsonUtility.FromJson<Data>(saveData);
    }
}

[System.Serializable]
public class Data
{
    public float MasterVolume, MusicVolume, SoundEffectVolume;

    public void SaveSettings(float master, float music, float soundEffect)
    {
        MasterVolume = master;
        MusicVolume = music;    
        SoundEffectVolume = soundEffect;     
    }
}