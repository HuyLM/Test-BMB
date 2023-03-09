using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Collections;

public static class UnityHelper
{
    public static readonly Vector2 Down = new Vector2(0.01f, -0.99f);
    public static readonly string strNull = "null";

    public static Vector3 RotateDirection(this Vector3 baseDirection, float angle)
    {
        return (Quaternion.AngleAxis(angle, Vector3.back) * baseDirection).normalized;
    }

    public static Vector3[] ArrayVec2To3(Vector2[] v2)
    {
        Vector3[] result = new Vector3[v2.Length];
        for (int i = 0; i < result.Length; ++i)
        {
            result[i] = new Vector3(v2[i].x, v2[i].y, 0);
        }
        return result;
    }

    public static bool RandomPercent(int percent)
    {
        int random = UnityEngine.Random.Range(0, 100);
        return random < percent;
    }

    public static bool RandomPercent(float percent)
    {
        int random = UnityEngine.Random.Range(0, 100);
        return random < percent;
    }

    public static void DeleteImmediateAllChilds(this Transform t)
    {
        int count = t.childCount;
        for (int i = 0; i < count; i++)
            UnityEngine.Object.DestroyImmediate(t.GetChild(0).gameObject);
    }

    public static List<Vector3> FlipList(this List<Vector3> l)
    {
        return l.Select(s => new Vector3(-s.x, s.y)).ToList();
    }

    public static T ChangeAlpha<T>(this T g, float newAlpha)
         where T : Graphic
    {
        var color = g.color;
        color.a = newAlpha;
        g.color = color;
        return g;
    }

    public static T ChangeColor<T>(this T g, Color color)
         where T : Graphic
    {
        g.color = color;
        return g;
    }


    public static void Mute(UnityEngine.Events.UnityEventBase ev)
    {
        int count = ev.GetPersistentEventCount();
        for (int i = 0; i < count; i++)
        {
            ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
        }
    }

    public static void Unmute(UnityEngine.Events.UnityEventBase ev)
    {
        int count = ev.GetPersistentEventCount();
        for (int i = 0; i < count; i++)
        {
            ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        }
    }

    public static string CurrentNameScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
