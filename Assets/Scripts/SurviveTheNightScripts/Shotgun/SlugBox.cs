using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugBox : MonoBehaviour
{
    [SerializeField] private GameObject closedBox;
    [SerializeField] private GameObject openedBox;
    private bool isOpened = false;
    //Switch from closed box to opened box
    public void OpenBox() { 
        closedBox.SetActive(false);
        openedBox.SetActive(true);
        isOpened = true;
    }

    public void CloseBox()
    {
        closedBox.SetActive(true);
        openedBox.SetActive(false);
        isOpened = false;
    }

    public void SetActivationState(bool activate) { 
        if (activate)
        {
            OpenBox();
        }
    }
    
}
