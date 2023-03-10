using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, ObjectWaveSpawnable
{
    public int health = 100;
    public int maxHealth = 100;
    public float atkRate = 2; // so lan atk tren 1 giay

    // move appear
    public MoveInfo appearMoveInfo;
    public virtual void CallSpawned()
    {
        // startMove Appear
        Appear();
    }


    protected virtual void Appear()
    { 
        transform.DOLocalPath(appearMoveInfo.)
    }


}
