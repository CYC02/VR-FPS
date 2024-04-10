using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float speed = 50f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { 
            //Death
            transform.gameObject.SetActive(false);
        }
        float currentHealth = health / maxHealth;
        GameObject healthBar = GetComponentInChildren<HealthBar>().gameObject;
        if (healthBar != null) {
            Slider healthBarSlider = healthBar.GetComponentInChildren<Slider>();
            if (healthBarSlider != null) {
                healthBarSlider.value = currentHealth;
            }
        }
    }

    public void HealHealth(int heal)
    {
        if (health + heal >= maxHealth)
        {
            health = maxHealth;
        }
        else {
            health += heal;
        }
    }
}
