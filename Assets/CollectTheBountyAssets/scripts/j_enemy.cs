using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class j_enemy : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent moose;
    public GameObject mooseModel;
    public Animator mooseAnim;

    int health = 100;
    int damage = 10;




    // Start is called before the first frame update
    void Start()
    {
        moose = GetComponent<NavMeshAgent>();
        mooseAnim = GetComponentInChildren<Animator>();
        mooseAnim.SetFloat("moose_speed", 1);
        // Debug.Log(moose.speed);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moose.destination = player.position;
        // Debug.Log(moose.remainingDistance);

        if (moose.remainingDistance < 2)
        {
            mooseAnim.SetFloat("moose_speed", 0);
        }
        else
        {
            mooseAnim.SetFloat("moose_speed", 1);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }

    public void activateEnemy()
    {
        gameObject.SetActive(true);
    }
}
