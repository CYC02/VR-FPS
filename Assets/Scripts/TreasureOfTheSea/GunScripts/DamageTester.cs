using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public FishHealthAttribute player;
    public FishHealthAttribute enemy;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            player.DealDamage(enemy.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            enemy.DealDamage(player.gameObject);
        }
    }
}
