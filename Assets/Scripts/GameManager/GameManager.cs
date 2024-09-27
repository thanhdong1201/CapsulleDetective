using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputReaderSO Input;

    [Header("Inventory")]
    public Inventory Inventory;

    [Header("Quest")]
    public QuestReceiver QuestReceiver;
    public QuestUI QuestUI;

    [Header("UI")]
    public UIManager UIManager;

    [Header("Audio")]
    public AudioClipSO AudioClipSO;
    public AudioManager AudioManager;

    [Header("SaveGame")]
    public JsonSaveSystem JsonSaveSystem;

    private void Awake()
    {
        Instance = this;
    }
    

}
