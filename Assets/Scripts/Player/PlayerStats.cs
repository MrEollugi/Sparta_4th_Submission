using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;

public class PlayerStats : MonoBehaviour
{
    #region Health Variables

    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    public bool isDead => currentHealth <= 0;

    #endregion

    #region Stamina Variables

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    private float currentStamina = 100f;
    private float staminaRegenRate = 10f;
    private float staminaRegenDelay = 1.0f;

    private float lastStaminaUsedTime = -999f;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //테스트용 키 입력 처리
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(10f);
        if (Input.GetKeyDown(KeyCode.J)) Heal(10f);

        // 스태미나 자연 회복 처리
        if(Time.time >= lastStaminaUsedTime + staminaRegenDelay)
        {
            RecoverStamina(Time.deltaTime * staminaRegenRate);
        }
    }

    #endregion

    #region Health Methods

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, maxHealth);

        if (isDead)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void Die()
    {
        // 아직 미구현
    }

    #endregion

    #region Stamina Methods

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public float GetMaxStamina()
    {
        return maxStamina;
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
    }

    public void UseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0f, maxStamina);
        lastStaminaUsedTime = Time.time;
    }

    public void RecoverStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
    }

    #endregion
}

