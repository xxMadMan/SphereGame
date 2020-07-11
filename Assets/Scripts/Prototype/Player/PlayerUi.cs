using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerUi : MonoBehaviour
{
    public Transform playerHealthUI;
    Image healthSlider;


    void Start()
    {
        healthSlider = playerHealthUI.GetChild(0).GetComponent<Image>();

        GetComponent<CharacterStats>().OnHealthChanged += OnHPChanged;
    }

    void OnHPChanged(int maxHealth, int currentHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercent;
    }

}
