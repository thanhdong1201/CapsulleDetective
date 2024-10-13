using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class QuestDataWindow : EditorWindow
{
    private List<QuestSO> questSOData = new List<QuestSO>();
    private QuestSO selectedQuestSO;
    private Vector2 scrollPos;

    private GUIStyle boldStyle_1;
    private GUIStyle boldStyle_2;

    private string newFileName = "1_Quest";
    private string customPath = "";
    private int selectedIndex = -1;

    [MenuItem("Window/Quest Data Aggregator")]
    public static void ShowWindow()
    {
        GetWindow<QuestDataWindow>("Quest Data Aggregator");
    }
    private void OnEnable()
    {
        LoadAllMyData();
        SetUpFontStyle();

        selectedQuestSO = Selection.activeObject as QuestSO;
    }
    private void LoadAllMyData()
    {
        questSOData.Clear();
        string[] guids = AssetDatabase.FindAssets("t:QuestSO");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            QuestSO data = AssetDatabase.LoadAssetAtPath<QuestSO>(path);
            if (data != null)
            {
                questSOData.Add(data);
            }
        }
    }
    private void SetUpFontStyle()
    {
        boldStyle_1 = new GUIStyle();
        boldStyle_1.fontStyle = FontStyle.Bold;
        boldStyle_1.fontSize = 16;
        boldStyle_1.normal.textColor = Color.white;
        boldStyle_1.alignment = TextAnchor.MiddleCenter;

        boldStyle_2 = new GUIStyle();
        boldStyle_2.fontStyle = FontStyle.Bold;
        boldStyle_2.fontSize = 14;
        boldStyle_2.normal.textColor = Color.white;
        boldStyle_2.alignment = TextAnchor.MiddleCenter;
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Choose Path...", GUILayout.Width(150)))
        {
            string path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject", newFileName, "asset", "Please choose a file path.");
            if (!string.IsNullOrEmpty(path)) customPath = path;
            EditorPrefs.SetString("CustomPath", customPath);
        }
        if (GUILayout.Button("Create New", GUILayout.Width(150)))
        {
            CreateNewQuestSO();
        }
        if (GUILayout.Button("Refresh", GUILayout.Width(150)))
        {
            RefreshData();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Current Path: ", customPath);

        //
        GUILayout.BeginHorizontal();

        //RightSide
        GUILayout.BeginHorizontal("Box", GUILayout.Width(200));
        GUILayout.BeginVertical(GUILayout.Width(200));
        GUILayout.Label(" List of Quest ", boldStyle_1);
        GUILayout.Space(10);
        DisplayQuestSOWindow();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        //LeftSide
        GUILayout.BeginHorizontal("Box");
        GUILayout.BeginVertical();
        if (selectedQuestSO != null)
        {
            DisplayQuestsData(selectedQuestSO);
        }
        else
        {
            GUILayout.Label("");
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();
    }
    private void DisplayQuestSOWindow()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (questSOData != null && questSOData.Count > 0)
        {
            foreach (QuestSO data in questSOData)
            {
                int i = questSOData.IndexOf(data);
                if (selectedIndex == i)
                {
                    GUI.backgroundColor = Color.cyan;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }
                if (GUILayout.Button(questSOData[i].name, GUILayout.Height(30)))
                {
                    selectedIndex = i;
                    selectedQuestSO = questSOData[i];
                }
            }
            GUI.backgroundColor = Color.white;
        }
        EditorGUILayout.EndScrollView();
    }
    private void DisplayQuestsData(QuestSO data)
    {
        GUILayout.Label(data.name, boldStyle_1);
        GUILayout.Space(10);
        data.QuestName = EditorGUILayout.TextField("Quest Name", data.QuestName);
        data.ItemSO = (ItemSO)EditorGUILayout.ObjectField("Item Needed", data.ItemSO, typeof(ItemSO), allowSceneObjects: false);
        data.ItemReward = (ItemSO)EditorGUILayout.ObjectField("Reward", data.ItemReward, typeof(ItemSO), allowSceneObjects: false);
        data.DialogSO = (DialogSO)EditorGUILayout.ObjectField("Dialog", data.DialogSO, typeof(DialogSO), allowSceneObjects: false);
        data= (QuestSO)EditorGUILayout.ObjectField("QuestSO", data, typeof(QuestSO), allowSceneObjects: false);
        //data.AcceptQuestDialogs[0] = EditorGUILayout.TextField("Accept Quest Dialog", data.AcceptQuestDialogs[0]);
        GUILayout.Label("File Path: " + AssetDatabase.GetAssetPath(data));
        GUILayout.Space(10);
        EditorUtility.SetDirty(data);

        string path = AssetDatabase.GetAssetPath(data);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Rename"))
        {
            RenameFile(path, data.QuestName);
        }
        if (GUILayout.Button("Delete"))
        {
            if (EditorUtility.DisplayDialog("Confirm Delete", $"Are you sure you want to delete {data.QuestName}?", "Yes", "No"))
            {
                DeleteFile(data);
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    private void RenameFile(string filePath, string newName)
    {
        if (string.IsNullOrEmpty(newName))
        {
            Debug.LogWarning("New file name is empty.");
            return;
        }
        AssetDatabase.RenameAsset(filePath, newName);
        AssetDatabase.SaveAssets();
    }
    private void CreateNewQuestSO()
    {
        QuestSO newData = CreateInstance<QuestSO>();
        selectedQuestSO = newData;
        AssetDatabase.CreateAsset(newData, customPath);
        AssetDatabase.SaveAssets();
        RefreshData();
    }
    private void DeleteFile(QuestSO data)
    {
        string path = AssetDatabase.GetAssetPath(data);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        RefreshData();
    }
    private void RefreshData()
    {
        selectedQuestSO = null;
        selectedIndex = -1;
        LoadAllMyData();
        AssetDatabase.Refresh();
    }
}
