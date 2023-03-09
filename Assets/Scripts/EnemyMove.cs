using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class EnemyMove : Data
{
    public bool loopPath;
    public bool isCuver;
    public float delayTimeStart;
    public float durationMove;
    public List<Vector2Serializable> totalPaths = new List<Vector2Serializable>();

    [JsonIgnore]
    public override bool IsEmpty
    {
        get
        {
            return loopPath == default
            && isCuver == default
            && delayTimeStart == default
            && durationMove == default
            && totalPaths == default;
        }
    }

   

    public override void Reset()
    {
        loopPath = default;
        isCuver = default;
        delayTimeStart = default;
        durationMove = default;
      
        totalPaths = default;
       
    }
    public override JToken ToJObject()
    {
        return JToken.FromObject(this);
    }
}
