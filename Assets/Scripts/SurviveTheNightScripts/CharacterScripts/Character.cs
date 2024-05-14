using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool isDead = false;
    public HealthBar healthBar;

    protected virtual void Awake()
    {
        //healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //healthBar = GetComponentInChildren<HealthBar>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        if (healthBar == null) {
            healthBar = GetComponentInChildren<HealthBar>();
        }

        health -= damage;
        if (health <= 0)
        {
            //Death
            //transform.gameObject.SetActive(false);
            health = 0;
            isDead = true;
            healthBar.ChangeHealthBarSliderValue(0f, maxHealth);
        }
        else {
            if (healthBar != null)
            {
                healthBar.ChangeHealthBarSliderValue(health, maxHealth);
            }
            else {
                Debug.LogError("HealthBar is missing");
            }            

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
