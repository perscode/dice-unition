using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public float force;
    private Rigidbody2D playerRigidBody;
    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Knockback(Collision2D collision)
    {
        playerRigidBody.AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
