using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int currentHealth;
    public int totalHealth;
    public GameObject winScreen;
    public static bool bossIsDead = false;
    private EnemyHealthBar healthBar;
    public WinScreen WinScript;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;

        healthBar = GetComponentInChildren<EnemyHealthBar>();
        //healthBar.SetEnemyMaxHealth(totalHealth);
        healthBar.SetEnemyHealth(currentHealth);
        GetComponent<WinScreen>().OnWin();
        
    }

    public void DamageBoss(int BossDamage)
    {
        currentHealth = currentHealth - BossDamage;
        if (currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDead");
            Destroy(gameObject);
            bossIsDead = true;
            WinScript.OnWin();
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
        healthBar.SetEnemyHealth(currentHealth);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);

    }


}

