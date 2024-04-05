using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.NativeTypes;
using UnityEngine.XR.Interaction.Toolkit;


/*
 * Author: Cindy Chan
 * 
 */
public class SlugAmmo : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable interactable;

    private void Start()
    {
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Handle the event when an interactor selects the object
        IXRSelectInteractor interactor = args.interactorObject;
        Debug.Log("Object selected by interactor: " + interactor);
        GameObject shotgun = GameObject.FindWithTag("Shotgun");
        if (shotgun != null)
        {
            Shotgun shotgunComponent = shotgun.GetComponent<Shotgun>();
            if (shotgunComponent != null && interactor is XRDirectInteractor) {
                shotgunComponent.ShowSlugDropZoneMesh(true);
                Debug.Log("ammo selected");
            }
        }
        else {
            Debug.LogError("Could not find shotgun in the scene");
        }
    }

    private void OnSelectExited(SelectExitEventArgs args) {
        GameObject shotgun = GameObject.FindWithTag("Shotgun");
        if (shotgun != null)
        {
            Shotgun shotgunComponent = shotgun.GetComponent<Shotgun>();
            if (shotgunComponent != null)
            {
                shotgunComponent.ShowSlugDropZoneMesh(false);
            }
        }
        else
        {
            Debug.LogError("Could not find shotgun in the scene");
        }
    }
}
