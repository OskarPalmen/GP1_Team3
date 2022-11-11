using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    


    private void Update()
    {
        if (PauseMenu.isPaused) {
            return;
        }
        else if (BossHealth.bossIsDead) {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0)) {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                FindObjectOfType<AudioManager>().Play("Pang"); 
        }
    }
}

