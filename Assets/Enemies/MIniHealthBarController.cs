using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIniHealthBarController : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] HealthBar healthBar;
    private EnemyController enemyController;
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        healthBar.SetMaxHealth(enemyController.getHealth());
    }

    void Update()
    {
        currentHealth = enemyController.getHealth();
        healthBar.SetHealth(currentHealth);
    }
}
