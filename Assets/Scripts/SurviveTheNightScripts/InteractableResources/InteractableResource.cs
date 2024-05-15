using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Cindy Chan
//all Interactable resources will make a sound when dropped on the floor

[RequireComponent(typeof(AudioSource))]
public class InteractableResource : MonoBehaviour
{
    [SerializeField] protected AudioSource dropSound;
    void Start()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (dropSound != null && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            dropSound.Play();
        }
    }

}
