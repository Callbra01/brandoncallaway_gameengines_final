using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target transform
    Transform target;

    // Get obj tagged 'Player'
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Set camera transform to player's transform
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
