using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool isDead = false;
    private HealthBar healthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Death
            //transform.gameObject.SetActive(false);
            isDead = true;
        }
        else {
            //float currentHealth = (float) health / (float) maxHealth;
            //GameObject healthBar = GetComponentInChildren<HealthBar>().gameObject;
            /*
            if (healthBar != null) {
                Slider healthBarSlider = healthBar.GetComponentInChildren<Slider>();
                if (healthBarSlider != null) {
                    healthBarSlider.value = currentHealth;
                }
            }      
            */
            healthBar.ChangeHealthBarSliderValue(health,maxHealth);
        }

    }

    public virtual void HealHealth(int heal)
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
