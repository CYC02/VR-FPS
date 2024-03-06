using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//source reference: https://www.youtube.com/watch?v=d2X-2nR_6WY
//unity reference: https://docs.unity3d.com/Manual/xr_input.html

/*
 This script is used for debugging and identifying the XR controllers and inputs.
 */
public class InputInfo : MonoBehaviour
{
    private enum ControllerSide 
    {
        Left_Controller,
        Right_Controller
    }

    [SerializeField]
    private ControllerSide m_controller;

    private InputDeviceCharacteristics m_characteristics;

    // Start is called before the first frame update
    void Start() 
    {
        if (m_controller == ControllerSide.Left_Controller)
        {
            m_characteristics = InputDeviceCharacteristics.Left;
        }
        else
        {
            m_characteristics = InputDeviceCharacteristics.Right;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        List<InputDevice> m_device = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(m_characteristics, m_device);
        if (m_device.Count == 1) 
        {
            //One device found
            CheckController(m_device[0]);
        }
        else {
            if (m_characteristics == InputDeviceCharacteristics.Left)
            {
                Debug.Log("Left Controller not found");
            }
            else
            {
                Debug.Log("Right Controller not found");
            }
        }
    }

    private void CheckController(InputDevice d) 
    {
        bool primaryButtonDown = false;
        bool triggerButton = false;

        //primary Button Check
        if (d.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonDown) && primaryButtonDown)
        {
           Debug.Log("Primary Button is pressed.");
        }

        //trigger button check
        if (d.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton) && triggerButton)
        {
            Debug.Log("Trigger button is pressed.");
        }

    }
}
