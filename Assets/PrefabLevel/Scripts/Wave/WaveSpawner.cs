using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveSpawnObject[] spawnObjects;

    [SerializeField]private float timeCount;
    private bool enableSpawn;
    private Queue<WaveSpawnObject> spawnQueue;

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
        spawnQueue = new Queue<WaveSpawnObject>();
        for (int i = 0; i < spawnObjects.Length; ++i)
        {
            spawnObjects[i].Init();
            spawnQueue.Enqueue(spawnObjects[i]);
        }
        //temp
        StartWave();
    }

    public void StartWave()
    {
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
        WaveSpawnObject waveSpawnTime = spawnQueue.Peek();
        if (waveSpawnTime != null)
        {
            if (timeCount > waveSpawnTime.SpawnDelay)
            {
                SpawnObject(waveSpawnTime);
                if (spawnQueue.Count == 0)
                {
                    SpawnComplete();
                }
                else
                {
                    CheckSpawn();
                }
            }
        }
    }

    protected virtual void SpawnObject(WaveSpawnObject waveSpawnTime)
    {
        waveSpawnTime.Spawn();
        spawnQueue.Dequeue();
    }

    protected virtual void SpawnComplete()
    {
        SetEnableSpawn(false);
        EndWave();
    }

    public virtual void EndWave()
    {

    }
}
