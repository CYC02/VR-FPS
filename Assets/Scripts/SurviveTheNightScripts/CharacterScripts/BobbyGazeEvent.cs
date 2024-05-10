using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BobbyGazeEvent : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor leftHandSocket;
    [SerializeField] private XRSocketInteractor rightHandSocket;
    public bool isHovered;
    public void OnHovered(bool hovered) {
        isHovered = hovered;
        //Debug.Log("is hovered: " + isHovered);
    }

    public XRSocketInteractor GetLeftHandSocket() {
        return leftHandSocket;
    }

    public XRSocketInteractor GetRightHandSocket() {
        return rightHandSocket;
    }
}
