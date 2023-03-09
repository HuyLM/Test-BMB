using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRespawn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float botPoint = -22 * transform.localScale.y;
        if (transform.position.y < botPoint)
        {
            float respawnPoint = 22 * transform.localScale.y;
            transform.position = new Vector3(transform.position.x, respawnPoint + (transform.position.y - botPoint), transform.position.z);
        }
    }
}
