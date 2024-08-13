using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    List<GameObject> bulletPool = new List<GameObject>();
    public int poolSize = 20;

    public float speed = 20;

    public bool shooting = false;

    private void Start()
    {
        bulletSpawnPoint = GetComponent<Transform>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && shooting)
        {
            GameObject bullet = Take();
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * speed; 
        }
    }

    private GameObject Take()
    {
        foreach (GameObject @object in bulletPool)
        {
            if (!@object.activeSelf)
            {
                @object.transform.position = bulletSpawnPoint.transform.position;
                @object.SetActive(true);
                return @object;
            }
        }
        return null;
    }
}
