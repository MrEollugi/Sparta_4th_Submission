using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightSettingInitializer : MonoBehaviour
{
    #region Ambient Light Settings

    [Header("Ambient Light Settings")]
    [SerializeField] private Color ambientColor = new Color(0.2f, 0.2f, 0.25f);

    #endregion

    #region Directional Light Settings

    [Header("Main Light Settings")]
    [SerializeField] private Light mainLight;
    [SerializeField] private Color lightColor = Color.yellow;
    [SerializeField] private float intensity = 0.6f;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        // Set ambient environment lighting
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = ambientColor;

        // Set directional light (sun) settings
        if (mainLight != null)
        {
            mainLight.color = lightColor;
            mainLight.intensity = intensity;
            mainLight.shadows = LightShadows.Soft;
        }
    }

    #endregion
}
