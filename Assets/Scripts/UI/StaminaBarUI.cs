using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Image staminaImage;

    private void LateUpdate()
    {
        if (Player.Instance != null && staminaImage != null)
        {
            float stamina = Player.Instance.Stats.GetCurrentStamina();
            float maxStamina = Player.Instance.Stats.GetMaxStamina();
            staminaImage.fillAmount = stamina / maxStamina;
        }
    }
}
