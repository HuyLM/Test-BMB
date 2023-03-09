using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnTime : MonoBehaviour
{
    [SerializeField] private float spawnDelay;

    public float SpawnDelay { get => spawnDelay; }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }
}
