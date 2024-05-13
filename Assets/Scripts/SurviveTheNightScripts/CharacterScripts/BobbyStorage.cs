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
    [SerializeField] private int maxBackpackStorage = 2;
    public int amountInBackpack = 0;
    //private List<GameObject> items;
    //public GameObject objectInLeftHand;
    //public GameObject objectInRightHand;
    private bool isWearingBackpack = false;
    public GameObject backpack;
    private GameObject dropItem;
    public GameObject getResourceButton;
    public bool isBackpackFull = false;

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
                    if (backpack != null)
                    {   
                        amountInBackpack++;

                        itemToAdd.transform.SetParent(dropItem.transform);
                        Rigidbody rb = itemToAdd.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        itemToAdd.transform.localPosition= Vector3.zero;
                        itemToAdd.SetActive(false);

                        //check if backpack is full
                        if (amountInBackpack == maxBackpackStorage) {
                            isBackpackFull = true;
                            getResourceButton.GetComponent<Button>().interactable = false;
                        }
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
            isBackpackFull = false;
            GameObject firstItemInBag = dropItem.transform.GetChild(0).gameObject;
            firstItemInBag.SetActive(true);
            firstItemInBag.transform.localPosition = Vector3.zero;
            firstItemInBag.transform.SetParent(null);

            Rigidbody rb = firstItemInBag.GetComponent<Rigidbody>();
            rb.isKinematic = false;

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
