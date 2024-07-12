using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    private Image StaminaBar;
    public float CurrentStamina;
    public float MaxStamina;
    PlayerMovement player;
    
    private void Start()
    {
        StaminaBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        CurrentStamina = player.stamina;
        MaxStamina = player.maxStamina;
        StaminaBar.fillAmount = CurrentStamina / MaxStamina;
    }
}