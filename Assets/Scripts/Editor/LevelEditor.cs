using DG.Tweening;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    private string pathResources = @"Assets/Resources/";
    private string pathMoveFolder = @"Maps/Move/";
    private string pathDataFolder = @"Maps/Data/";

    private LevelData levelCacheData;
    private string levelNameCreating = "Map-Level-";
    private string[] bars = { "Editor", "Creator" };
    private int toolSelected;

    private EnemyData currentEnemyData;

    private DrawPathHelper pathHelper;
    private DrawPathHelper PathHelper
    {
        get
        {
            if (pathHelper == null)
            {
                GameObject gameObject = new GameObject("Path Helper");
                gameObject.hideFlags |= HideFlags.DontSave;
                pathHelper = gameObject.AddComponent<DrawPathHelper>();
            }
            return pathHelper;
        }
    }

    private void Awake()
    {
        Load();
    }

    private void OnDestroy()
    {
        if (pathHelper)
        {
            DestroyImmediate(pathHelper.gameObject);
        }
    }

    [MenuItem("Tools/Level Editor %l")]
    private static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<LevelEditor>();
        window.titleContent = new GUIContent("Level Editor");
        window.Show();
    }

    private void Load()
    {
        TextAsset data = Resources.Load<TextAsset>(pathDataFolder + levelNameCreating);
        Load(data);
    }

    private void Load(TextAsset statTextAsset)
    {
        if (statTextAsset == null)
        {
            return;
        }

        JArray statJsonData = JArray.Parse(statTextAsset.text);

        LevelData levelCacheData = new LevelData();
        levelCacheData.statTextData = statTextAsset;

        TextAsset moveTextAsset = Resources.Load<TextAsset>($"{pathMoveFolder}{statTextAsset.name}");
        if (moveTextAsset != null)
        {
            JArray moveJsonData = JArray.Parse(moveTextAsset.text);

            for (int i = 0; i < statJsonData.Count; i++)
            {
                EnemyData cacheData = new EnemyData();
                EnemyStat enemyStatData = statJsonData[i].ToObject<EnemyStat>();
                EnemyMove enemyMoveData = moveJsonData[i].ToObject<EnemyMove>();

                cacheData.stat = enemyStatData;
                cacheData.move = enemyMoveData;

                levelCacheData.enemies.Add(cacheData);
            }
            levelCacheData.moveTextData = moveTextAsset;

        }
        this.levelCacheData  = levelCacheData;
    }

    private void Save()
    {
        if (levelCacheData == null)
        {
            return;
        }
        Save(levelCacheData);
    }

    private void Save(LevelData levelCacheData)
    {

        JArray jarrayStat = new JArray();
        JArray jarrayMove = new JArray();
        for(int i = 0; i < levelCacheData.enemies.Count; ++i)
        {
            JToken fdf = levelCacheData.enemies[i].stat.ToJObject();
            JToken fdf232 = levelCacheData.enemies[i].move.ToJObject();
            jarrayStat.Add(levelCacheData.enemies[i].stat.ToJObject());
            jarrayMove.Add(levelCacheData.enemies[i].move.ToJObject());
        }

        string statDataFileName = levelCacheData.statTextData.name;
        string statMoveFileName = levelCacheData.moveTextData.name;
        string statDataText = jarrayStat.ToString();
        string moveDataText = jarrayMove.ToString();


        WriteToFile($"{pathResources}{pathDataFolder}{statDataFileName}", ".txt", statDataText.ToString());
        WriteToFile($"{pathResources}{pathMoveFolder}{statMoveFileName}", ".txt", moveDataText.ToString());
    }

    private void OnGUI()
    {
        int oldBar = toolSelected;
        toolSelected = GUILayout.Toolbar(toolSelected, bars, GUILayout.Height(30));
        if (oldBar != toolSelected)
        {
            if (toolSelected == 1)
            {
                PathHelper.Reset();
                SceneView.RepaintAll();
            }
        }
        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
        GUILayout.Label("* * * * *", EditorStyles.centeredGreyMiniLabel);

        if (levelCacheData == null)
        {
            GUILayout.Label("No data, please reload!", EditorStyles.whiteLargeLabel);
        }

        switch (toolSelected)
        {
            case 1:
                Create();
                break;
            default:
                Edit();
                break;
        }
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Save", "Do you want to save data. This may take a few minutes.", "Ok", "Cancel"))
            {
                Save();
            }
        }
        if (GUILayout.Button("Reload", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Reload", "Do you want to reload data. This may take a few minutes.", "Ok", "Cancel"))
            {
                Load();
            }
        }
        GUILayout.EndHorizontal();
    }

    private void Create()
    {
        levelNameCreating = EditorGUILayout.TextField("File name", levelNameCreating);
        GUILayout.Space(10);
        GUI.enabled = false;
        EditorGUILayout.TextField("File Data", $"{pathResources}{pathDataFolder}{levelNameCreating}.txt");
        EditorGUILayout.TextField("File Move", $"{pathResources}{pathMoveFolder}{levelNameCreating}.txt");
        GUI.enabled = true;

        GUILayout.Space(10);

        if (GUILayout.Button("Create", GUILayout.Height(30)))
        {
            bool exits = ExitsFile($"{pathResources}{pathDataFolder}{levelNameCreating}.txt");

            if (exits)
            {
                if (EditorUtility.DisplayDialog("Exits", "Do you want to overwrite the file data?", "Ok", "Cancel"))
                {
                    TextAsset newAsset = WriteToFile($"{pathResources}{pathDataFolder}{levelNameCreating}", ".txt", "[]");
                    Load(newAsset);
                }
            }
            else
            {
                TextAsset newAsset = WriteToFile($"{pathResources}{pathDataFolder}{levelNameCreating}", ".txt", "[]");
                WriteToFile($"{pathResources}{pathMoveFolder}{levelNameCreating}", ".txt", "[]");
                Load(newAsset);
            }
        }
    }

    private void Edit()
    {

        if (levelCacheData == null)
            return;

        GUILayout.Space(10);
                    ShowFoldoutDataLayout($"{levelCacheData.moveTextData.name}",
                        () => {
                            for (int i = 0; i < levelCacheData.enemies.Count; i++)
                            {
                                ShowElementFoldoutDataLayout($" {i + 1} - Enemy {levelCacheData.enemies[i].stat.EnemyId}",
                                    () => {
                                        currentEnemyData = levelCacheData.enemies[i];
                                        ShowEnemyStatData(currentEnemyData.stat);
                                        ShowEnemyMoveData(currentEnemyData.move);
                                    },
                                    () => { },
                                    i);
                            }
                        },
                        () => Save(levelCacheData),
                        () => { levelCacheData.enemies.Add(new EnemyData()); },
                        0);
    }

    private void ShowFoldoutDataLayout(string title, Action show, Action save, Action add, int foldoutIndex)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        bool showing = true;
        bool foldout = showing;
        foldout = EditorGUILayout.Foldout(foldout, title, true);
        if (foldout)
        {
            if (GUILayout.Button("Save", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                if (EditorUtility.DisplayDialog("Save", "Do you want to save data", "Ok", "Cancel"))
                {
                    save?.Invoke();
                }
            }
            if (GUILayout.Button("Add", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                add?.Invoke();
            }
        }

        EditorGUILayout.EndHorizontal();
        if (foldout)
        {
            if (!showing)
            {
            }
            show?.Invoke();
        }
        else if (showing)
        {
            PathHelper.Reset();
            SceneView.RepaintAll();
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowElementFoldoutDataLayout(string title, Action show, Action remove, int foldoutIndex)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        bool showing = true;
        bool foldout = showing;
        foldout = EditorGUILayout.Foldout(foldout, title, true);
        if (GUILayout.Button("-", GUILayout.Width(25)))
        {
            remove?.Invoke();
        }
        EditorGUILayout.EndHorizontal();
        if (foldout)
        {
            if (!showing)
            {
            }
            show?.Invoke();
        }
        else if (showing)
        {
            PathHelper.Reset();
            SceneView.RepaintAll();
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowEnemyMoveData(EnemyMove data)
    {
        Color color = GUI.color;
        GUI.color = Color.gray;
        GUILayout.Button(string.Empty, GUILayout.Height(4));
        GUI.color = color;
        GUILayout.Space(5);

        data.loopPath = EditorGUILayoutToggle("Loop Path", data.loopPath);
        data.isCuver = EditorGUILayoutToggle("Is Curve", data.isCuver);
        data.delayTimeStart = EditorGUILayoutFloatField("Delay Time Start", data.delayTimeStart);
        data.durationMove = EditorGUILayoutFloatField("Duration Move", data.durationMove);
        ShowListVector2("Paths", data.totalPaths, 0);
    }

    private void ShowEnemyStatData(EnemyStat data)
    {
        Color color = GUI.color;
        GUI.color = Color.gray;
        GUILayout.Button(string.Empty, GUILayout.Height(4));
        GUI.color = color;
        GUILayout.Space(5);

        data.Hp = EditorGUILayoutFloatField("Health Point", data.Hp);
        data.EnemyId = EditorGUILayoutIntField("Enemy Id", data.EnemyId);
    }
    private void ShowListVector2(string title, List<Vector2Serializable> data, int foldoutIndex)
    {
        if (data == null)
        {
            data = new List<Vector2Serializable>();
        }

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        bool showing = true;
        bool foldout = showing;
        foldout = EditorGUILayout.Foldout(foldout, title, true);
        if (foldout)
        {
            if (GUILayout.Button("Edit", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                PathHelper.paths = data;
                PathHelper.levelCacheData = levelCacheData;
                PathHelper.pathType = currentEnemyData.move.isCuver ? PathType.CatmullRom : PathType.Linear;
                Selection.activeGameObject = PathHelper.gameObject;
                SceneView.RepaintAll();
            }
            if (GUILayout.Button("Add", EditorStyles.toolbarButton, GUILayout.Width(50)))
            {
                data.Add(new Vector2());
            }
        }
        EditorGUILayout.EndHorizontal();

        if (foldout)
        {
            if (PathHelper.paths != data)
            {
                PathHelper.paths = data;
                PathHelper.levelCacheData = levelCacheData;
                PathHelper.pathType = currentEnemyData.move.isCuver ? PathType.CatmullRom : PathType.Linear;
                Selection.activeGameObject = PathHelper.gameObject;
                SceneView.RepaintAll();
            }

            int removeIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                Vector2 vector = data[i];
                if (ShowVector2(i, ref vector))
                {
                    removeIndex = i;
                }
                data[i] = vector;
            }

            if (removeIndex != -1)
            {
                data.RemoveAt(removeIndex);
            }
        }

        if (foldout)
        {
            if (!showing)
            {
            }
        }
        else if (showing)
        {
            PathHelper.Reset();
            SceneView.RepaintAll();
        }
        EditorGUILayout.EndVertical();
    }

    private bool ShowVector2(int index, ref Vector2 data)
    {
        EditorGUILayout.BeginHorizontal("Box");

        data.x = EditorGUILayout.FloatField("X" + index, data.x);
        data.y = EditorGUILayout.FloatField("Y" + index, data.y);
        bool remove = GUILayout.Button("-", GUILayout.Width(25));

        EditorGUILayout.EndHorizontal();
        return remove;
    }

    private float EditorGUILayoutFloatField(string title, float value)
    {
            return EditorGUILayout.FloatField(title, value);

        if (false || value != default)
        {
            return EditorGUILayout.FloatField(title, value);
        }
        return value;
    }

    private int EditorGUILayoutIntField(string title, int value)
    {
        return EditorGUILayout.IntField(title, value);

        if (false || value != default)
        {
            return EditorGUILayout.IntField(title, value);
        }
        return value;
    }

    private bool EditorGUILayoutToggle(string title, bool value)
    {
        return EditorGUILayout.Toggle(title, value);

        if (false || value != default)
        {
            return EditorGUILayout.Toggle(title, value);
        }
        return value;
    }


    private bool ExitsFile(string path)
    {
        return File.Exists(path);
    }

    private TextAsset WriteToFile(string path, string extension, string data)
    {
        TextAsset asset = null;
        if (!ExitsFile(path))
        {
            asset = new TextAsset(data);
            AssetDatabase.CreateAsset(asset, path + extension);
        }
        else
        {
            asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        }

        using (StreamWriter file = File.CreateText(path + extension))
        {
            file.WriteLine(data);
        }

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        return asset;
    }
}
