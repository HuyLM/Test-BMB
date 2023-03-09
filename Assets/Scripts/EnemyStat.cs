using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class EnemyStat : Data
{
    public int EnemyId;
    public float Hp;

    [JsonIgnore]
    public override bool IsEmpty
    {
        get
        {
            return EnemyId == default
            && Hp == default;
        }
    }

    public override void Reset()
    {
        EnemyId = default;
        Hp = default;
    }

    public override JToken ToJObject()
    {
        return JToken.FromObject(this);
    }
}
