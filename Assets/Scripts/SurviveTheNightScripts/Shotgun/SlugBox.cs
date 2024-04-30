using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/*
 * Author: Cindy Chan
 * This script is used for making the box open and give the player ammo.
 */
public class SlugBox : MonoBehaviour
{
    [SerializeField] private GameObject closedBox;
    [SerializeField] private GameObject openedBox;
    [SerializeField] private XRGrabInteractable interactable;
    [SerializeField] private GameObject slugAmmoPrefab;
    private bool isOpened = false;
    [SerializeField] private int maxAmmoInBox = 12;
    private int ammoInBox;  
    private void Start()
    {
        ammoInBox = maxAmmoInBox;
    }

    //Switch from closed box to opened box
    public void OpenBox() { 
        closedBox.SetActive(false);
        openedBox.SetActive(true);
        isOpened = true;
    }

    //Activate event triggers this function to open the box
    public void OnActivate(bool activate) {
        if (activate)
        {
            OpenBox();
        }
        if (isOpened) {
            Vector3 offsetPos = new Vector3(0,0,0.1f);
            Instantiate(slugAmmoPrefab, transform.position + offsetPos, Quaternion.identity);
            ammoInBox -= 1;
            if (ammoInBox == 0) {
                Destroy(gameObject);
            }
        }
    }

}
