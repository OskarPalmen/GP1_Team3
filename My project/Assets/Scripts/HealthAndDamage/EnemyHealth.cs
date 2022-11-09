using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;
    private EnemyHealthBar healthBar;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;

        healthBar = GetComponentInChildren<EnemyHealthBar>();
        healthBar.SetEnemyMaxHealth(totalHealth);
        healthBar.SetEnemyHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DamageEnemy(int enemyDamage)
    {
        currentHealth = currentHealth - enemyDamage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.SetEnemyHealth(currentHealth);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);

    }


}
