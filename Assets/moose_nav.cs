using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moose_nav : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent moose;

    // Start is called before the first frame update
    void Start()
    {
        moose = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        moose.destination = player.position;
    }
}
