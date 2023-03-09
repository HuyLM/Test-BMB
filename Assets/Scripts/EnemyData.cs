using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyData 
{
    public EnemyStat stat;
    public EnemyMove move;

    public EnemyData()
    {
        stat = new EnemyStat();
        move = new EnemyMove();


    }
}

public abstract class Data
{
    [JsonIgnore] public abstract bool IsEmpty { get; }
    public abstract void Reset();
    public abstract JToken ToJObject();
}
