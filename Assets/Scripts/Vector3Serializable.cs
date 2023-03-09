using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Serializable
{
    public float x, y, z;
    public Vector3Serializable(float rX, float rY, float rZ = 0)
    {
        x = rX;
        y = rY;
        z = rZ;
    }
    public static implicit operator Vector3(Vector3Serializable rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }
    public static implicit operator Vector3Serializable(Vector3 rValue)
    {
        return new Vector3Serializable(rValue.x, rValue.y, rValue.z);
    }
}
[Serializable]
public class Vector2Serializable
{
    public float x, y;
    public Vector2Serializable(float rX, float rY)
    {
        x = rX;
        y = rY;
    }
    public static implicit operator Vector2(Vector2Serializable rValue)
    {
        return new Vector3(rValue.x, rValue.y);
    }
    public static implicit operator Vector2Serializable(Vector2 rValue)
    {
        return new Vector2Serializable(rValue.x, rValue.y);
    }
    public static implicit operator Vector2Serializable(Vector3 rValue)
    {
        return new Vector2Serializable(rValue.x, rValue.y);
    }
    public static implicit operator Vector3(Vector2Serializable rValue)
    {
        return new Vector3(rValue.x, rValue.y);
    }
}
[Serializable]
public class QuaternionSerializable
{
    public float x, y, z, w;
    public QuaternionSerializable(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }
    public static implicit operator Quaternion(QuaternionSerializable rValue)
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
    public static implicit operator QuaternionSerializable(Quaternion rValue)
    {
        return new QuaternionSerializable(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}
