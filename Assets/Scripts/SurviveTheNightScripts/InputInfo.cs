using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//source reference: https://www.youtube.com/watch?v=d2X-2nR_6WY
//source reference: https://www.youtube.com/watch?v=Kh_94glqO-0
//unity reference: https://docs.unity3d.com/Manual/xr_input.html

/*
 * Author: Cindy Chan
 * This script is used for debugging and identifying the XR controllers and inputs.
 */
public class InputInfo : MonoBehaviour
{
    public InputDevice rightController;
    public InputDevice leftController;
    public InputDevice headMountDisplay;

    public XRDirectInteractor leftControllerInteractor;
    public XRDirectInteractor rightControllerInteractor;

    private GameObject player;

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

        player = GameObject.FindGameObjectWithTag("Player");
        leftControllerInteractor = player.transform.GetChild(0).GetChild(1).GetComponent<XRDirectInteractor>();
        rightControllerInteractor = player.transform.GetChild(0).GetChild(2).GetComponent<XRDirectInteractor>();
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
                //Debug.Log("Left Controller not found");
            }
            else
            {
               // Debug.Log("Right Controller not found");
            }
        }

        if (!rightController.isValid || !leftController.isValid || !headMountDisplay.isValid) {
            InitializeInputDevices();
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

    private void InitializeInputDevices() {
        if (!rightController.isValid) {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);
        }
        if (!leftController.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref leftController);
        }
        if (!headMountDisplay.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref headMountDisplay);
        }
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice) { 
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        if (devices.Count > 0) { 
            inputDevice= devices[0];
        }
    }

    public XRDirectInteractor GetLeftXRDirectInteractor() {
        return leftControllerInteractor;
    }

    public XRDirectInteractor GetRightXRDirectInteractor() {
        return rightControllerInteractor;
    }
}
