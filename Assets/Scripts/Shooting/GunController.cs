using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private float bulletSpeed = 20f;

    private Transform bulletSpawnPoint;
    private List<GameObject> bulletPool = new List<GameObject>();
    public bool shooting = false;

    private void Start()
    {
        bulletSpawnPoint = transform;

        InitializeBulletPool();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && shooting)
        {
            Shoot();
        }
    }

    private void InitializeBulletPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Shoot()
    {
        GameObject bullet = GetInactiveBullet();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }

    private GameObject GetInactiveBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        return null;
    }
}
