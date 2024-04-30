using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
