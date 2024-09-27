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
        Debug.Log(filePath);
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

//[System.Serializable]
//public class Dataa
//{
//    public bool isLogged;
//    public string email;
//    public string password;
//    public List<string> feedTimer;
//    public string lightTimer;
//    public string lightTimerOff;

//    public void SaveAccount(string emailData, string passwordData)
//    {
//        email = emailData;
//        password = passwordData;
//    }
//    public void SaveFeedTimer(string timerData)
//    {
//        feedTimer.Add(timerData);
//    }
//    public void RemoveFeedTimer(int index)
//    {
//        feedTimer.RemoveAt(index);
//    }
//    public void SaveLightTimer(string timerData)
//    {
//        lightTimer = timerData;
//    }
//    public void SaveLightTimerOff(string timerData)
//    {
//        lightTimerOff = timerData;
//    }
//    public void RemoveLightTimer()
//    {
//        lightTimer = "";
//    }
//    public void RemoveLightTimerOff()
//    {
//        lightTimerOff = "";
//    }
//}