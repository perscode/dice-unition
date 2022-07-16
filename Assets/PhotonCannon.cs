using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCannon : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject photonProjectile;
    public float photonSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KeepShootimg());
    }

    private void fire(float speed)
    {
        speed *= photonSpeed;
        GameObject shot = Instantiate(photonProjectile, transform.position, Quaternion.identity);
        ShotMovement shotMovement = shot.GetComponent<ShotMovement>();
        shotMovement.bulletVelocity = new Vector2(speed + playerMovement.Velocity.x, 0);
    }

    IEnumerator KeepShootimg()
    {
        WaitForSeconds longInterval = new WaitForSeconds(2);
        WaitForSeconds shortInterval = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return longInterval;
            fire(-1);
            yield return shortInterval;
            fire(1);
        }
    }
}
