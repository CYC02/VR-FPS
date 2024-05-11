using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Cindy Chan
 * This script manages Bobby's storage.
 */
public class BobbyStorage : MonoBehaviour
{
    [SerializeField] int maxBackpackStorage = 10;
    private int amountInBackpack = 0;
    List<GameObject> items;
    GameObject objectInLeftHand;
    GameObject objectInRightHand;
    private bool isWearingBackpack = false;
    private void Start()
    {
    }

    // When Bobby is getting resources, Bobby can store the items in the backpack storage.
    // Can only call this function if Bobby is wearing the backpack
    public void AddItemToBackpack(GameObject itemToAdd) {
        if (itemToAdd)
        {
            if (isWearingBackpack)
            {
                if (amountInBackpack < maxBackpackStorage)
                {
                    items.Add(itemToAdd);
                    amountInBackpack++;
                }
                else
                {
                    Debug.Log("Backpack is full. Cannot add anymore items.");
                }
            }
            else {
                Debug.LogError("Cannot add item to backpack if Bobby is not wearing it.");
            }
        }
        else {
            Debug.LogError("itemToAdd is null");
        }

    }

    //function is trigger from the select event of the Backpack socket interactor
    public void WearingBackpack(bool isWearing) {
        isWearingBackpack = isWearing;
    }
}
