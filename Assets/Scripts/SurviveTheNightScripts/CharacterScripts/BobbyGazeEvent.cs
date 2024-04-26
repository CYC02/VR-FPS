using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BobbyGazeEvent : MonoBehaviour
{
    public bool isHovered;
    public void OnHovered(bool hovered) {
        isHovered = hovered;
    }
}
