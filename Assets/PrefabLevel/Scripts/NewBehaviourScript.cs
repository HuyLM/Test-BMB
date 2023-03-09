using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private DOTweenPath dotweenPath;

    // Start is called before the first frame update
    void Start()
    {
        float duration;
        Ease ease;
        int loops;
        PathType pathType;
        bool closePath;
        bool localMovement;
        PathMode pathMode;
        AxisConstraint axisConstraint;
        OrientType orientType;
        List<Vector3> paths = dotweenPath.wps;
        List<Vector3> fpaths = dotweenPath.fullWps;

        Debug.Log("-----------path-----------");
        foreach(var p in paths)
        {
            Debug.Log(p);
        }

        Debug.Log("-----------ffffffffffffpath-----------");
        foreach (var p in fpaths)
        {
            Debug.Log(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
