using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorChanger : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer playerSprite; // Reference to your player's sprite
    public Image colorPreview;         // UI Image for color preview
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    [Header("Settings")]
    public Color defaultColor = Color.white;

    private Color currentColor;

    private void Start()
    {
        // Initialize with default color
        currentColor = defaultColor;
        UpdateSliders();
        ApplyColor();
    }

    public void OnColorChanged()
    {
        // Get values from sliders
        currentColor.r = redSlider.value;
        currentColor.g = greenSlider.value;
        currentColor.b = blueSlider.value;

        ApplyColor();
    }

    private void ApplyColor()
    {
        // Apply to player sprite
        if (playerSprite != null)
        {
            playerSprite.color = currentColor;
        }

        // Update preview
        if (colorPreview != null)
        {
            colorPreview.color = currentColor;
        }
    }

    private void UpdateSliders()
    {
        if (redSlider != null) redSlider.value = currentColor.r;
        if (greenSlider != null) greenSlider.value = currentColor.g;
        if (blueSlider != null) blueSlider.value = currentColor.b;
    }

    // Optional: Save/Load color
    public void SaveColor()
    {
        PlayerPrefs.SetFloat("PlayerColorR", currentColor.r);
        PlayerPrefs.SetFloat("PlayerColorG", currentColor.g);
        PlayerPrefs.SetFloat("PlayerColorB", currentColor.b);
    }

    public void LoadColor()
    {
        if (PlayerPrefs.HasKey("PlayerColorR"))
        {
            currentColor.r = PlayerPrefs.GetFloat("PlayerColorR");
            currentColor.g = PlayerPrefs.GetFloat("PlayerColorG");
            currentColor.b = PlayerPrefs.GetFloat("PlayerColorB");
            UpdateSliders();
            ApplyColor();
        }
    }
}
