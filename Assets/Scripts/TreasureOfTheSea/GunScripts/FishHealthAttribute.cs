using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHealthAttribute : MonoBehaviour
{
    public int health;
    public int attack;
    //public Transform enemy;

    public void takeDamage(int damage)
    {
        health -= damage;
        //if (health - damage <= 0) {
            //Death animation
            //Destroy(enemy);
        //}

        //if (health - damage < 50 && health - damage > 0) { 
            //normal swimming animation
        //}
        
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<FishHealthAttribute>();
        if(atm != null)
        {
            atm.takeDamage(attack);
        }
    }
}
