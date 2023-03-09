using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawPathHelper : MonoBehaviour
{
    public List<Vector2Serializable> paths;
    public LevelData levelCacheData;
    public PathType pathType;

    public void Reset()
    {
        paths = null;
        levelCacheData = null;
    }
}

[CustomEditor(typeof(DrawPathHelper))]
public class DrawPathEditor : Editor
{
    private DrawPathHelper pathHelper;


    private void Awake()
    {
        pathHelper = target as DrawPathHelper;
    }

    private void OnSceneGUI()
    {
        if (pathHelper.paths != null && pathHelper.paths.Count > 0)
        {
            for (int i = 0; i < pathHelper.paths.Count; i++)
            {
                pathHelper.paths[i] = Handles.PositionHandle(pathHelper.paths[i], Quaternion.identity);
            }

            List<Vector2> paths = new List<Vector2>();
            for(int p = 0; p < pathHelper.paths.Count; ++p)
            {
                paths.Add(pathHelper.paths[p]);
            }
           
            pathHelper.transform.position = paths[0];
            var tween = pathHelper.transform.DOPath(UnityHelper.ArrayVec2To3(paths.ToArray()), 1, pathHelper.pathType, PathMode.TopDown2D);
            tween.ForceInit();
            Vector3[] points = tween.PathGetDrawPoints();
            tween.Kill();
            if (points != null)
            {
                Color color = Handles.color;
                Handles.color = Color.red;
                for (int j = 0; j < points.Length; j++)
                {
                    if (j > 0)
                    {
                        Handles.DrawLine(points[j - 1], points[j], 3);
                    }
                }
                Handles.color = color;
            }

        }

        if (pathHelper.levelCacheData != null && pathHelper.levelCacheData.enemies != null && pathHelper.levelCacheData.enemies.Count > 0)
        {
            for (int i = 0; i < pathHelper.levelCacheData.enemies.Count; i++)
            {
                EnemyData cacheData = pathHelper.levelCacheData.enemies[i];
                List<Vector2> paths = new List<Vector2>();
                for (int p = 0; p < cacheData.move.totalPaths.Count; ++p)
                {
                    paths.Add(cacheData.move.totalPaths[p]);
                }

                if (paths != null && paths.Count > 0)
                {
                    pathHelper.transform.position = paths[0];
                    var tween = pathHelper.transform.DOPath(UnityHelper.ArrayVec2To3(paths.ToArray()), 1, pathHelper.pathType, PathMode.TopDown2D);
                    tween.ForceInit();
                    Vector3[] points = tween.PathGetDrawPoints();
                    tween.Kill();
                    if (points != null)
                    {
                        for (int j = 0; j < points.Length; j++)
                        {
                            if (j > 0)
                            {
                                Handles.DrawLine(points[j - 1], points[j]);
                            }
                        }
                    }
                }
            }
        }
    }
}
