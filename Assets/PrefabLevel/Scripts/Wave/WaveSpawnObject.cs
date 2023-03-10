using DG.Tweening;
using InspectorGadgets.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawnObject : MonoBehaviour
{
    [Header("For Design Level")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private MoveInfo appearMoveInfo;

    [Header("For Developer")]
    [SerializeField] private ObjectWaveSpawnable myObjectSpawn;

    public float SpawnDelay { get => spawnDelay; }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        if(myObjectSpawn != null)
        {
            myObjectSpawn.CallSpawned();
        }
    }

    [ContextMenu("ShowDotweenPath")]
    private void ShowDotweenPath()
    {
        DOTweenPath dOTweenPath = gameObject.GetComponent<DOTweenPath>();
        if (dOTweenPath == null)
        {
            dOTweenPath = gameObject.AddComponent<DOTweenPath>();
        }
        dOTweenPath.duration = appearMoveInfo.speed;
        dOTweenPath.isSpeedBased = true;
        dOTweenPath.easeType = appearMoveInfo.ease;
        dOTweenPath.loops = appearMoveInfo.loops;
        dOTweenPath.pathType = appearMoveInfo.pathType;
        dOTweenPath.isClosedPath = appearMoveInfo.isClosePath;
        dOTweenPath.isLocal = appearMoveInfo.isLocalMovement;
        dOTweenPath.pathMode = appearMoveInfo.pathMode;
        dOTweenPath.lockRotation = appearMoveInfo.lockRotation;
        dOTweenPath.orientType = appearMoveInfo.orientation;
        List<Vector3> paths = appearMoveInfo.paths.ToList();
        dOTweenPath.wps = paths;
    }

    [ContextMenu("ApplyDotweenPath")]
    private void ApplyDotweenPath()
    {
        DOTweenPath dOTweenPath = gameObject.GetComponent<DOTweenPath>();
        if (dOTweenPath == null)
        {
            return;
        }
        appearMoveInfo.speed = dOTweenPath.duration;
        appearMoveInfo.ease = dOTweenPath.easeType;
        appearMoveInfo.loops = dOTweenPath.loops;
        appearMoveInfo.pathType = dOTweenPath.pathType;
        appearMoveInfo.isClosePath = dOTweenPath.isClosedPath;
        appearMoveInfo.isLocalMovement = dOTweenPath.isLocal;
        appearMoveInfo.pathMode = dOTweenPath.pathMode;
        appearMoveInfo.lockRotation = dOTweenPath.lockRotation;
        appearMoveInfo.orientation = dOTweenPath.orientType;
        appearMoveInfo.paths = dOTweenPath.wps.ToArray();
        DestroyImmediate(dOTweenPath);
    }

}
