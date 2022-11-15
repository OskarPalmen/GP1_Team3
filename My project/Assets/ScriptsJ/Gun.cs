using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float shotCooldown = 0.2f;

    private bool cooldown = false;


    private void Update()
    {
        if (PauseMenu.isPaused) {
            return;
        }
        else if (BossHealth.bossIsDead) {
            return;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !cooldown) {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            FindObjectOfType<AudioManager>().Play("Pang");
            StartCoroutine(CooldownTimer());
        }
    }

    IEnumerator CooldownTimer()
    {
        cooldown = true;
        yield return new WaitForSeconds(shotCooldown);
        cooldown = false;
    }
}

