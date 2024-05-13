using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: Cindy Chan
 * This script manages Bobby's storage.
 */
public class BobbyStorage : MonoBehaviour
{
    [SerializeField] private int maxBackpackStorage = 10;
    private int amountInBackpack = 0;
    //private List<GameObject> items;
    //public GameObject objectInLeftHand;
    //public GameObject objectInRightHand;
    private bool isWearingBackpack = false;
    public GameObject backpack;
    private GameObject dropItem;
    public GameObject getResourceButton;

    private void Start()
    {
        //items = new List<GameObject>();
        backpack = GameObject.FindWithTag("Backpack");
        dropItem = backpack.transform.GetChild(1).gameObject;
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
                    //items.Add(itemToAdd);
                    amountInBackpack++;
                    if (backpack != null)
                    {
                        Rigidbody rb = itemToAdd.GetComponent<Rigidbody>();
                        rb.isKinematic = true;

                        //GameObject dropItem = backpack.transform.GetChild(2).gameObject;
                        itemToAdd.transform.SetParent(dropItem.transform);

                        itemToAdd.SetActive(false);
                    }
                    else {
                        Debug.LogError("backpack is null");
                    }
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

    public void DropItemFromBackpack() {

        if (dropItem.transform.childCount > 0)
        {
            Debug.Log("drop item from bag");
            amountInBackpack--;
            GameObject firstItemInBag = dropItem.transform.GetChild(0).gameObject;
            firstItemInBag.SetActive(true);
            firstItemInBag.GetComponent<Rigidbody>().isKinematic = false;
            firstItemInBag.transform.SetParent(null);
        }
        else {
            Debug.LogWarning("Nothing to drop from the backpack");
        }
    }

    //function is trigger from the select event of the Backpack socket interactor
    public void WearingBackpack(bool isWearing) {
        isWearingBackpack = isWearing;
        if (getResourceButton) {
            getResourceButton.GetComponent<Button>().interactable = isWearing;
        }
    }

    public bool IsWearingBackpack() { return isWearingBackpack; }
}
