using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGravity : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = new Vector3(0, -1f, 0);
    }
}
