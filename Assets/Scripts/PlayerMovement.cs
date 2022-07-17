using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUpgradable
{
    public float maxSpeed = 2.5f;
    public Vector3 Velocity = Vector3.zero;
    public float UpgradeFactor = 0.2f;
    public float UpgradeValue = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Velocity = Velocity.normalized * (maxSpeed + UpgradeValue * UpgradeFactor);
        Vector3 pos = transform.position;
        pos = pos + Velocity * Time.deltaTime;
        transform.position = pos;
    }

    public void SetValue(float value)
    {
        UpgradeValue = value;
    }
}
