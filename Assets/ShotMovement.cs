using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovement : MonoBehaviour
{
    public Vector2 bulletVelocity;

    void Update()
    {
        Vector2 pos = transform.position;
        pos += bulletVelocity * Time.deltaTime;
        transform.position = pos;
    }
}
