using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveInfo
{
    public float speed = 5;
    public Ease ease = Ease.Linear;
    public int loops = 1;
    public PathType pathType = PathType.Linear;
    public bool isClosePath = false;
    public bool isLocalMovement = true;
    public PathMode pathMode = PathMode.TopDown2D;
    public AxisConstraint lockRotation = AxisConstraint.None;
    public OrientType orientation = OrientType.None;
    public Vector3[] paths;

    public Tweener DoMove(Transform transform)
    {
        if(isLocalMovement)
        {
            return transform.DOLocalPath(
                 paths, 
                speed,
                pathType,
                pathMode
                )
                .SetEase(ease)
                .SetLoops(loops);
        }
    }
}
