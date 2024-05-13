using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestructor : MonoBehaviour
{
    public FishHealthAttribute enemy;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(gameObject);
        enemy.takeDamage(50);
    }
}