using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class j_enemy : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent moose;
    public GameObject mooseModel;
    Animator mooseAnim;

    int health = 100;
    int damage = 10;




    // Start is called before the first frame update
    void Start()
    {
        moose = GetComponent<NavMeshAgent>();
        mooseAnim = mooseModel.GetComponent<Animator>();
        mooseAnim.SetBool("moose_walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        moose.destination = player.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
            // Debug.Log("Attacked!");
        }
    }
}
