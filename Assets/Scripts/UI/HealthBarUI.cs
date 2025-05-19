using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private void Update()
    {
        if(Player.Instance != null)
        {
            float hp = Player.Instance.GetCurrentHealth();
            float maxHp = Player.Instance.GetMaxHealth();
            healthBar.value = hp / maxHp;
        }
    }
}
