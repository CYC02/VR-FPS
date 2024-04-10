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

    private void Start()
    {
        //interactable.hoverExited.AddListener(OnHoverExit);
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
    }

    //Hover exit event triggers this function put a slug in the hand
    public void OnHoverExit(HoverExitEventArgs args) {
        IXRHoverInteractor interactor = args.interactorObject;
        Debug.Log("Hovering: " + interactor);
        XRDirectInteractor directInteractor= interactor as XRDirectInteractor;
        if (isOpened) {
            //hand has ammo
            /*
            if (directInteractor != null) {
                GameObject ammoInstance = Instantiate(slugAmmoPrefab);
                if (ammoInstance) {
                    directInteractor.firstInteractableSelected = ammoInstance;
                }
            }
            */
        }
    }
}
