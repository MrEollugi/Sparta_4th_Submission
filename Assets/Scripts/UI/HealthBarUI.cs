using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;

    private void LateUpdate()
    {
        if(Player.Instance != null && healthImage != null)
        {
            float hp = Player.Instance.GetCurrentHealth();
            float maxHp = Player.Instance.GetMaxHealth();
            healthImage.fillAmount = hp / maxHp;
        }
    }
}
