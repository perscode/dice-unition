using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
