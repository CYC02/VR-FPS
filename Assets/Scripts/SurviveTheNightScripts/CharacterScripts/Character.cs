using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool isDead = false;

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
            //transform.gameObject.SetActive(false);
            isDead= true;
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
