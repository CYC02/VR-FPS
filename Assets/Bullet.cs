/**
 * MICHAEL CALLE
 * VR Project
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    /**
     * Upon creation, destroy the bullet after some time
     */
    void Awake()
    {
        Destroy(gameObject, life);
    }
    
    /**
     * What to do during a collision
     */
    void OnCollisionEnter(Collision collision)
    {
        //Ensures the bullets can only destroy the bird gameobjects
        if (collision.gameObject.GetComponent<FlockAgent>() != null) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
