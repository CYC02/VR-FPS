using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j_enemy : MonoBehaviour
{
    int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            health -= 20;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
            // Debug.Log("Attacked!");
        }
    }
}
