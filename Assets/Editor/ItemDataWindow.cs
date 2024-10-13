using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class ItemDataWindow : EditorWindow
{
    private List<ItemSO> itemSOData = new List<ItemSO>();
    private List<QuestSO> questSOData = new List<QuestSO>();
    private ItemSO selectedItemSO;
    private QuestSO selectedQuestSO;
    private Vector2 scrollPos;

    private GUIStyle boldStyle_1;
    private GUIStyle boldStyle_2;

    private string newFileName = "1_Item";
    private string customPath = "";
    private int selectedIndex = -1;
    private enum DataType
    {
        Item, Quest
    }
    private DataType selectedType;

    [MenuItem("Window/Item Data Window")]
    public static void ShowWindow()
    {
        GetWindow<ItemDataWindow>("Item Data Aggregator");
    }
    private void OnEnable()
    {
        LoadAllMyData();
        SetUpFontStyle();

        selectedItemSO = Selection.activeObject as ItemSO;
    }
    private void LoadAllMyData()
    {
        itemSOData.Clear();
        string[] guids = AssetDatabase.FindAssets("t:ItemSO");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemSO data = AssetDatabase.LoadAssetAtPath<ItemSO>(path);
            if (data != null)
            {
                itemSOData.Add(data);
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
            CreateNewItemSO();
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
        GUILayout.Label(" List of ItemSO ", boldStyle_1);
        GUILayout.Space(10);
        DisplayItemSOWindow();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        //LeftSide
        GUILayout.BeginHorizontal("Box");
        GUILayout.BeginVertical();
        if (selectedItemSO != null)
        {
            DisplayItemsData(selectedItemSO);
        }
        else
        {
            GUILayout.Label("");
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();
    }
    private void DisplayItemSOWindow()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (itemSOData != null && itemSOData.Count > 0)
        {
            foreach (ItemSO data in itemSOData)
            {
                int i = itemSOData.IndexOf(data);
                if (selectedIndex == i)
                {
                    GUI.backgroundColor = Color.cyan;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }
                if (GUILayout.Button(itemSOData[i].name, GUILayout.Height(30)))
                {
                    selectedIndex = i;
                    selectedItemSO = itemSOData[i];
                }
            }
            GUI.backgroundColor = Color.white;
        }
        EditorGUILayout.EndScrollView();
    }
    private void DisplayItemsData(ItemSO data)
    {
        GUILayout.Label(data.name, boldStyle_1);
        GUILayout.Space(10);
        data.ItemName = EditorGUILayout.TextField("Name", data.ItemName);
        data.Description = EditorGUILayout.TextField("Description", data.Description);
        data.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", data.Sprite, typeof(Sprite), allowSceneObjects: false);
        data.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", data.Prefab, typeof(GameObject), allowSceneObjects: false);
        //data = (ItemSO)EditorGUILayout.ObjectField("ItemSO", data, typeof(ItemSO), allowSceneObjects: false);
        GUILayout.Label("File Path: " + AssetDatabase.GetAssetPath(data));
        GUILayout.Space(10);
        EditorUtility.SetDirty(data);

        string path = AssetDatabase.GetAssetPath(data);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Rename"))
        {         
            RenameFile(path, data.ItemName);
        }
        if (GUILayout.Button("Delete"))
        {
            if (EditorUtility.DisplayDialog("Confirm Delete", $"Are you sure you want to delete {data.ItemName}?", "Yes", "No"))
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
    private void CreateNewItemSO()
    {
        ItemSO newData = CreateInstance<ItemSO>();
        selectedItemSO = newData;
        AssetDatabase.CreateAsset(newData, customPath);
        AssetDatabase.SaveAssets();
        RefreshData();
    }
    private void DeleteFile(ItemSO data)
    {
        string path = AssetDatabase.GetAssetPath(data);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        RefreshData();
    }
    private void RefreshData()
    {
        selectedItemSO = null;
        selectedIndex = -1;
        LoadAllMyData();
        AssetDatabase.Refresh();
    }
}

