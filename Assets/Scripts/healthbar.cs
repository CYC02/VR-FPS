using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarsprite;

    private Camera _cam;

    void start()
    {
        _cam = Camera.main;
    }
    public void updatehealth(float maxhealth, float currentHealth)
    {
        _healthbarsprite.fillAmount = currentHealth / maxhealth;

    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position);
    }
}
