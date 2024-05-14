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
    public bool isEnemyNear;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bobby")) {
            isBobbyNear = true;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Resource")) {
            NearResource nearResource = collider.gameObject.GetComponentInChildren<NearResource>();
            if (nearResource != null)
            {
                nearResource.nearPlayer = true;
            }
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            isEnemyNear = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bobby"))
        {
            isBobbyNear = false;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            NearResource nearResource = collider.gameObject.GetComponentInChildren<NearResource>();
            if (nearResource != null)
            {
                nearResource.nearPlayer = false;
            }
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isEnemyNear = false;
        }
    }
}
