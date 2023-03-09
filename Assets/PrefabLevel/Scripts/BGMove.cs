using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 originPosition;


    [Header("Editor")]
    [SerializeField] private float time;

    private void OnValidate()
    {
        if(Application.isPlaying == false)
        {
            if (time >= 0)
            {
                transform.position = originPosition + Vector3.down * speed * time;
            }
        }
       
    }

    private Transform myTransform;
    void Start()
    {
        //SetMove();
        myTransform = transform;
    }

    private void Update()
    {
        myTransform.position = myTransform.position + Vector3.down * speed * Time.deltaTime;
    }
}
