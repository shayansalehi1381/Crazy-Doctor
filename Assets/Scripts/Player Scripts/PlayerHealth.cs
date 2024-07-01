using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float Health = 100f;

    private PlayerMovement playerMovement;

    [SerializeField]
    private Slider HealthSlider;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (Health <= 0)
            return;

        Health -= damageAmount;
        
        if (Health <= 0)
        {
            playerMovement.PlayerDied();
            GamePlayController.instance.RestartGame();
        }
        HealthSlider.value = Health;
    }
}
