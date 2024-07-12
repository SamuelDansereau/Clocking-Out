using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    public float MaxHealth;
    PlayerMovement player;
    
    private void Start()
    {
        HealthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        CurrentHealth = player.health;
        MaxHealth = player.maxHealth;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
}
