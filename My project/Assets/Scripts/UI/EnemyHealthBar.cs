using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetEnemyMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void SetEnemyHealth(int health)
    {
        slider.value = health;
    }
}
