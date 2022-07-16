using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandller : MonoBehaviour
{
    public int health = 0;
    public bool isÍmmortal = false;
    public float invulnPeriod = 0;
    float invulnTimer = 0;
    public float thrust;
    private Rigidbody2D playerRigidBody;
    public string vulnerableTo;

    int correctLayer;

    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        correctLayer = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerRigidBody != null && collision.gameObject.tag == vulnerableTo) {
            invulnTimer = invulnPeriod;
            updateLayers(10);
            GameObject enemy = collision.gameObject;
            health--;

            Knockback(enemy);
            if (health <= 0 && !isÍmmortal)
            {
                Die();
            }
        }
    }

    private void updateLayers(int layer)
    {
        gameObject.layer = layer;
        foreach(Transform child in transform)
        {
            child.gameObject.layer = layer;
        }
    }

    private void Update()
    {
        invulnTimer -= Time.deltaTime;
        if (invulnTimer <= 0)
        {
            updateLayers(correctLayer);
        }
    }
    private void Knockback(GameObject enemyObject)
    {
        // .GetComponent<Rigidbody2D>()
        Vector2 difference = transform.position - enemyObject.transform.position;
        difference *= thrust;
        playerRigidBody.AddForce(difference, ForceMode2D.Impulse);
        Debug.Log("Player new position" + playerRigidBody.position);
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
