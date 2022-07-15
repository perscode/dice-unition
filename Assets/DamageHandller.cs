using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandller : MonoBehaviour
{
    public int playerHealth = 500;
    public bool isÍmmortal = false;
    public float thrust;
    private Rigidbody2D playerRigidBody;
    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (playerRigidBody != null && collision.gameObject.tag == "Enemy") {
            Debug.Log("Player Position" + playerRigidBody.position);
            GameObject enemy = collision.gameObject;
            playerHealth--;
            Knockback(enemy);
            if (playerHealth <= 0 && !isÍmmortal)
            {
                Die();
            }
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
