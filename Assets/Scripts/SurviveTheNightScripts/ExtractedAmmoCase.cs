using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * When the extracted slug ammo has been extracted, it will fall to the ground which has the "Ground" layer.
 * When the ammo casing hits the gameobject with the "Ground" layer, it will destroy itself after a few seconds.
 */
public class ExtractedAmmoCase : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        int groundLayerIdx = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == groundLayerIdx)
        {
            Invoke("DestroyAmmoCasing", destructionDelay);
        }
    }

    private void DestroyAmmoCasing()
    {
        Destroy(gameObject);
    }
}
