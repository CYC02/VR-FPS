using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranch : InteractableResource
{
    [SerializeField] private AudioSource stepTreeBranchSound;
    void Start()
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.tag == "Friendly" || collision.gameObject.tag == "Neutral" || collision.gameObject.tag == "Enemy") {
            stepTreeBranchSound.Play();
        }
    }

}
