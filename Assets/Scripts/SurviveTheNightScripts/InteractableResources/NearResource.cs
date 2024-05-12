using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This is used to detect if someone is near the resource.
 */

public class NearResource : MonoBehaviour
{
    public bool isBobbyNear;
    public bool nearPlayer;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bobby"))
        {
            isBobbyNear = true;
            //put resource into bag storage
            BobbyStorage bobbyStorage = collider.GetComponent<BobbyStorage>();
            if (bobbyStorage != null) {
                if (bobbyStorage.IsWearingBackpack())
                {
                    GameObject itemToAdd = transform.parent.gameObject;
                    if (itemToAdd != null)
                    {
                        if (itemToAdd.tag == "Bobby" || itemToAdd.tag == "Backpack")
                        {
                            Debug.LogWarning("Cannot add Bobby or backpack to the bag");
                        }
                        else {
                            bobbyStorage.AddItemToBackpack(itemToAdd);
                        }
                    }
                }
                else {
                    Debug.LogWarning("Bobby is not wearing his backpack. Cannot collect resource.");
                }
            }
            else {
                Debug.LogError("Bobby is missing bobbyStorage script");            
            }

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
