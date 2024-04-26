using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This is used to detect if someone is near the player by using collison triggers.
 */

public class NearPlayer : MonoBehaviour
{
    public bool isBobbyNear;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bobby")) {
            isBobbyNear = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bobby"))
        {
            isBobbyNear = false;
        }
    }
}
