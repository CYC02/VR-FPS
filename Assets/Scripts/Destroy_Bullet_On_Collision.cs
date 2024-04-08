using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestructor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(gameObject);
    }
}