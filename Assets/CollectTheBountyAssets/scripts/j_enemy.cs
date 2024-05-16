using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Jenny Ton
// All things related to the moose, AI, animation, bullet collision, win message
// Could have been organized better

public class j_enemy : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent moose;
    public GameObject moose_model;
    public GameObject health_textfield;
    public GameObject end_textfield;
    public GameObject big_canvas;
    public Button reset_button;
    public Button start_button;

    int moose_health = 100;
    int damage = 10;

    Animator moose_anim;
    TextMeshProUGUI health_text;
    TextMeshProUGUI end_text;


    // Start is called before the first frame update
    void Start()
    {
        reset_button.gameObject.SetActive(false);
        moose = GetComponent<NavMeshAgent>();
        moose_anim = GetComponentInChildren<Animator>();
        health_text = health_textfield.GetComponent<TextMeshProUGUI>();
        health_text.text = "H:" + moose_health;

        end_text = end_textfield.GetComponent<TextMeshProUGUI>();

        moose_anim.SetFloat("moose_speed", 1);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moose.destination = player.position;
        // Debug.Log(moose.remainingDistance);

        if (moose.remainingDistance < 2)
        {
            // mooseAnim.SetFloat("moose_speed", 0);
            moose_anim.SetTrigger("moose_charge");
            var position = new Vector3(player.position.x, gameObject.transform.position.y, player.position.z);
            gameObject.transform.LookAt(position);
        }
        else
        {
            moose_anim.SetFloat("moose_speed", 1);
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
                big_canvas.SetActive(true);
                start_button.gameObject.SetActive(false);
                reset_button.gameObject.SetActive(true);

                end_text.text = "You win! Restart the scene?";
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

    public void resetScene()
    {
        SceneManager.LoadScene("CollectTheBounty") ;
    }

}
