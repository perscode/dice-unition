using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 2.5f;
    public Vector3 Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, Input.GetAxis("Vertical") * maxSpeed);
        Vector3 pos = transform.position;
        pos = pos + Velocity * Time.deltaTime;
        transform.position = pos;
    }
}
