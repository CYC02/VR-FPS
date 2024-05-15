using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Cindy Chan
//Manages the health bar UI for characters
public class HealthBar : MonoBehaviour
{
    private Camera mainCamera;
    private Transform mainCameraTransform;
    void Start()
    {
        mainCamera= Camera.main;
        mainCameraTransform = mainCamera.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCameraTransform);
    }

    public void ChangeHealthBarSliderValue(float health, float maxHealth) {
        if (maxHealth == 0) {
            Debug.LogWarning("max health cannot be zero. cannot divide by zero");
            return;
        }

        float currentHealth = (float)health / (float)maxHealth;
        Slider healthBarSlider = GetComponentInChildren<Slider>();
        if (healthBarSlider != null)
        {   if (health <= 0) {
                healthBarSlider.value = 0f;
            }
            else {
                healthBarSlider.value = currentHealth;
            }
        }
    }
}
