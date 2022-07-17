using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCannon : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject photonProjectile;
    private float bulletSpeed;
    private float weaponSpeed = 2;
    public int weaponSpeedModifier = 1;
    private int bulletCount = 2;
    public int bulletCountModifier = 0;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 5f;
        StartCoroutine(KeepShootimg());
    }

    private void fire(float directionX, float directionY)
    {
        GameObject shot = Instantiate(photonProjectile, transform.position, Quaternion.identity);
        ShotMovement shotMovement = shot.GetComponent<ShotMovement>();
        shotMovement.bulletVelocity = new Vector2(directionX, directionY);
    }

    IEnumerator KeepShootimg()
    {
        int longInterval = 3;
        

        while (true)
        {
            int bulletsInMagazine = (bulletCount + bulletCountModifier) / 2;
            float angleStep = 360f / bulletsInMagazine;
            float angle = 90f;
            WaitForSeconds shortInterval = new WaitForSeconds(0.1f);
            float longDelay = longInterval - (float)((weaponSpeed + weaponSpeedModifier * 2) / 18);
            yield return new WaitForSeconds(longDelay);
            while (bulletsInMagazine-- > 0) {
                yield return shortInterval;
                float projectileDirXposition = playerMovement.Velocity.x + Mathf.Sin((angle * Mathf.PI) / 180);
                float projectileDirYposition = playerMovement.Velocity.y + Mathf.Cos((angle * Mathf.PI) / 180);
                Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                Vector2 projectileMoveDirection = (projectileVector - (Vector2)playerMovement.Velocity).normalized * bulletSpeed;
                fire(projectileMoveDirection.x, projectileMoveDirection.y);

                angle += angleStep;
            }
            bulletsInMagazine = (bulletCount + bulletCountModifier) / 2;
        }
    }

}
