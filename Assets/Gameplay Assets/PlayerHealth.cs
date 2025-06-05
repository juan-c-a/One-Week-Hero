using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Animator animator;
    public bool isDead = false;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log("Player Took Damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
            Debug.Log("Health Updated.");
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
        // Aqu� pod�s agregar l�gica como desactivar controles, mostrar men� de game over, etc.
    }
}
