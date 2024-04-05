using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * Author: Cindy Chan
 */
public class LeftHandEventFunctions : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor leftHandDirectInteractor;
    // Start is called before the first frame update
    private void Start()
    {
        leftHandDirectInteractor.selectEntered.AddListener(OnSelectShotgun);
        leftHandDirectInteractor.selectExited.AddListener(OnSelectExitShotgun);
    }

    /*
     * When the left controller direct interactor select an interactable, check it is the shotgun then invoke 
     * the shotgun's function to let it know that the left hand is selecting the gun.
    */
    private void OnSelectShotgun(SelectEnterEventArgs args) {
        IXRSelectInteractable interactable = args.interactableObject;
        //Debug.Log("Interactable selected with left hand " + interactable);
        if (interactable != null && interactable.transform.gameObject.CompareTag("Shotgun")) {
            Shotgun shotgunComponent = interactable.transform.gameObject.GetComponent<Shotgun>();
            if (shotgunComponent != null) {
                shotgunComponent.OnSelectedWithLeftHand();
            }
        }
    }


    /*
     * When the left controller direct interactor select an interactable, check it is the shotgun then invoke 
     * the shotgun's function to let it know that the left hand is selecting the gun.
    */
    private void OnSelectExitShotgun(SelectExitEventArgs args)
    {
        IXRSelectInteractable interactable = args.interactableObject;
        //Debug.Log("Interactable selected with left hand " + interactable);
        if (interactable != null && interactable.transform.gameObject.CompareTag("Shotgun"))
        {
            Shotgun shotgunComponent = interactable.transform.gameObject.GetComponent<Shotgun>();
            if (shotgunComponent != null)
            {
                shotgunComponent.OnSelectedExitedWithLeftHand();
            }
        }
    }
}
