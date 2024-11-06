using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    int maxAmmo = 50;
    int currentAmmo = 0;
    float shootCd = 0.1f;
    float bulletSpeed = 10f;
    Vector3 shootDirection;
    bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }

        if(transform.childCount <= 0) canShoot = true;
    }

    IEnumerator Shoot()
    {
        shootDirection = Input.mousePosition;
        shootDirection.z = 0;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection -= transform.position;

        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);
            Vector2 force = new Vector2(shootDirection.x, shootDirection.y).normalized * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().velocity = force;
            yield return new WaitForSeconds(shootCd);
        }
    }
}
