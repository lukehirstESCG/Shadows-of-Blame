using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Slider sunBrightnessSlider, moonBrightnessSlider;
    public TextMeshProUGUI brightnessValue;
    public Light sunlightIntenstity;
    public Light moonLightIntenstity;

    private void Awake()
    {
        sunBrightnessSlider.onValueChanged.AddListener(SetSunIntensity);
        moonBrightnessSlider.onValueChanged.AddListener(SetMoonIntensity);
    }

    private void Start()
    {
        sunBrightnessSlider.value = PlayerPrefs.GetFloat("sunBrightness", 1f);
        moonBrightnessSlider.value = PlayerPrefs.GetFloat("moonBrightness", 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("sunBrightness", sunBrightnessSlider.value);
        PlayerPrefs.SetFloat("moonBrightness", moonBrightnessSlider.value);
    }

    public void SetSunIntensity(float value)
    {
        sunlightIntenstity.intensity = value;
    }

    public void SetMoonIntensity(float value)
    {
        moonLightIntenstity.intensity = value;
    }
}
