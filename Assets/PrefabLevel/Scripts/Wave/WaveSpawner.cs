using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveSpawnTime[] spawnObjects;

    [SerializeField]private float timeCount;
    private bool enableSpawn;
    private Queue<WaveSpawnTime> spawnQueue;

    private void OnValidate()
    {
         Array.Sort(spawnObjects, (obj1, obj2)=> {
            return obj1.SpawnDelay.CompareTo(obj2.SpawnDelay);
        });
    }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        enableSpawn = false;
        timeCount = 0;
        spawnQueue = new Queue<WaveSpawnTime>();
        for (int i = 0; i < spawnObjects.Length; ++i)
        {
            spawnObjects[i].Init();
            spawnQueue.Enqueue(spawnObjects[i]);
        }
        SetEnableSpawn(true);
    }

    public void SetEnableSpawn(bool enable)
    {
        enableSpawn = enable;
    }


    private void Update()
    {
        if(enableSpawn)
        {
            timeCount += Time.deltaTime;
            CheckSpawn();
        }
    }

    private void CheckSpawn()
    {
        WaveSpawnTime waveSpawnTime = spawnQueue.Peek();
        if (waveSpawnTime != null)
        {
            if (timeCount > waveSpawnTime.SpawnDelay)
            {
                waveSpawnTime.Spawn();
                spawnQueue.Dequeue();
                if(spawnQueue.Count == 0)
                {
                    SetEnableSpawn(false);
                }    
                else
                {
                    CheckSpawn();
                }
            }
        }
    }
}
