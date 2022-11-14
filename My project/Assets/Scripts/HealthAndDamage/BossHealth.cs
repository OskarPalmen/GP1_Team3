using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;
    public static bool bossIsDead = false;
    private EnemyHealthBar healthBar;
    public WinScreen winScript;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;

        healthBar = GetComponentInChildren<EnemyHealthBar>();
        healthBar.SetEnemyMaxHealth(totalHealth);
        healthBar.SetEnemyHealth(currentHealth);
        winScript = FindObjectOfType<WinScreen>();
        winScript.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DamageBoss(int BossDamage)
    {
        currentHealth = currentHealth - BossDamage;
        if (currentHealth <= 0)
        {
            //FindObjectOfType<AudioManager>().Play("EnemyDead");
            
            bossIsDead = true;
            winScript.OnWin();
            Time.timeScale = 0;
            winScript.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        healthBar.SetEnemyHealth(currentHealth);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);

    }


}

