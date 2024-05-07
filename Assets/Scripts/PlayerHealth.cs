using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public TextMeshProUGUI healthText;
    public GameObject deathEffectPrefab;

    private bool isDead = false;

    private PlayerMovement playerMovement;
    private Animator animator;

    private GameObject deathEffectInstance; // Переменная для отслеживания клонированного объекта эффекта смерти

    private static readonly int DeathHash = Animator.StringToHash("Death");

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();

        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthText();
    }

    private void Die()
    {
        isDead = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Выключаем скрипт PlayerMovement
        }

        if (animator != null)
        {
            animator.SetTrigger(DeathHash);
            animator.SetBool("LoopDeath", false);
        }

        DestroyCharacter();
    }

    private void DestroyCharacter()
    {
        // Опционально: Вызываем метод для удаления персонажа
        Destroy(gameObject);

        // Удаляем клон объекта эффекта смерти, если он существует
        if (deathEffectInstance != null)
        {
            Destroy(deathEffectInstance);
        }

        Debug.Log("Персонаж умер!");
    }

    private void UpdateHealthText()
    {
        healthText.text = "Здоровье: " + currentHealth.ToString();
    }
}