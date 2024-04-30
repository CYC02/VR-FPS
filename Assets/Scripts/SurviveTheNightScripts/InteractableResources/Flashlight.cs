using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : InteractableResource
{
    [SerializeField] private Light light;
    [SerializeField] private AudioSource audio;
    void Start()
    {
        if (light == null)
        {
            light = GetComponentInChildren<Light>();
        }


        if (audio == null)
        {
            audio = GetComponentInChildren<AudioSource>();
        }

        SwitchLightOn();
    }

    public void SwitchLightOn() {
        light.enabled = !light.enabled;
        audio.Play();
    }
}
