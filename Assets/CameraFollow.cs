using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform myTarget;
    [Tooltip("Value 0-1"), Range(0, 1)]
    public float Elasticity = 0;
    // Update is called once per frame
    void Update()
    {
        if (myTarget != null) {
            //Vector3 targetPos = myTarget.position;
            // targetPos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, myTarget.position, 1-Elasticity);
        }
    }
}
