using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class j_enemy : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent moose;
    public GameObject mooseModel;
    public GameObject health_textfield;

    int moose_health = 100;
    int damage = 10;

    Animator mooseAnim;
    TextMeshProUGUI health_text;


    // Start is called before the first frame update
    void Start()
    {
        moose = GetComponent<NavMeshAgent>();
        mooseAnim = GetComponentInChildren<Animator>();
        health_text = health_textfield.GetComponent<TextMeshProUGUI>();
        health_text.text = "H:" + moose_health;

        mooseAnim.SetFloat("moose_speed", 1);
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
            moose_health -= damage;

            updateMooseHealth();

            if (moose_health <= 0)
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

    void updateMooseHealth()
    {
        health_text.text = "H:" + moose_health;
    }
}
