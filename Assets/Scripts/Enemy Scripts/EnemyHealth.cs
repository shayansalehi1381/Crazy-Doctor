using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    private float Health = 100f;

    [SerializeField]
    private Slider enemyHealthSlider;


    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (Health <= 0)   
            return;
        
        Health -= damageAmount;

     if (Health <= 0)
        {
            Health = 0;
            enemy.EnemyDied();
            EnemySpawner.instance.EnemyDied(gameObject);
            GamePlayController.instance.EnemyKilled(); 
        }

        enemyHealthSlider.value = Health;
    }
}
