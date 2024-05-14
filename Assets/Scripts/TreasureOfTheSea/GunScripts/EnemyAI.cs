using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 5f; // Movement speed of the enemy
    public float damping = 0.5f; // Damping factor for the movement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity for underwater movement
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate direction towards the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Calculate movement force
            Vector3 movement = direction * moveSpeed;

            // Apply the movement force with damping to avoid sudden stops
            rb.AddForce(movement, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            // You can add your attack logic here
        }
    }

}
