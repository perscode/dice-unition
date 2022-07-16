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
    SpriteRenderer spriteRenderer;

    int correctLayer;

    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        correctLayer = gameObject.layer;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("Object '" + gameObject.name + "' has no sprite renderer.");
            }
        }
        StartCoroutine(InvulnerabilityAnimation());
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
            if (child.gameObject.layer != 11) { 
                child.gameObject.layer = layer;
            }
        }
    }

    private void Update()
    {
        if (invulnTimer > 0) { 
            invulnTimer -= Time.deltaTime;


            if (invulnTimer <= 0)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
                updateLayers(correctLayer);
            }
        }
    }
    private void Knockback(GameObject enemyObject)
    {
        // .GetComponent<Rigidbody2D>()
        Vector2 difference = transform.position - enemyObject.transform.position;
        difference *= thrust;
        playerRigidBody.AddForce(difference, ForceMode2D.Impulse);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator InvulnerabilityAnimation()
    {
        WaitForSeconds shortInterval = new WaitForSeconds(0.1f);

        while (true)
        {
            if (invulnTimer > 0 && spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
            yield return shortInterval;
            
        }
    }
}
