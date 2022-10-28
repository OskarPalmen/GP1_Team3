using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Gun : MonoBehaviour
{
    InputManager inputManager;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
*/