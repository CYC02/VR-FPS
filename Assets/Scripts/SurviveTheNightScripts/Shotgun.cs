using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//reference: https://www.youtube.com/watch?v=j9F6kEB9rBg
//reference: https://www.youtube.com/watch?v=1gPLfY93JHk

/*
 * Author: Cindy Chan
 * This script manages the shotgun's shooting and other functionality.
 */
[RequireComponent(typeof(InputInfo))]
public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform foreend;
    private bool IsActivated = false;
    private XRGrabInteractable grabInteractable;
    private InputInfo inputInfo;

    // Start is called before the first frame update

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        inputInfo = GetComponent<InputInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //List<InputDevice> m_device = new List<InputDevice>();
        //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right, m_device);
        
        //if (m_device.Count == 1) {
            //InputDevice device = m_device[0];
            //One right side device found
            bool rightTriggerButtonDown = false;
            bool rightGripButtonDown = false;

            bool leftGripButtonDown = false;


            //if gripping the gun and pressed the trigger button, then shoot
            if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripButtonDown) && rightGripButtonDown)
            //if (device.TryGetFeatureValue(CommonUsages.gripButton, out rightGripButtonDown) && rightGripButtonDown)
            {
                //Debug.Log("Grip button pressed");
                //ensures that the shooting only occurs when activating/holding the shotgun
                if (IsActivated)
                {
                    if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerButtonDown) && rightTriggerButtonDown)
                    //if (device.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerButtonDown) && rightTriggerButtonDown)
                    {
                        Debug.Log("Right trigger button pressed");

                        Shoot();

                    }


                }
                //extracting bullet casing
                //ensure left hand grip is true
                if (inputInfo.leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripButtonDown) && leftGripButtonDown) {
                    //get velocity of left hand controller
                    if (inputInfo.leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
                    {
                        //if the left hand passes a certain forward velocity, move the fore-end up
                        //if the velocity passes a certain velocity backwards, move the fore-end down to the original position
                        float leftMag = leftVelocity.magnitude;
                        Debug.Log(leftMag);
                        if (leftMag > 2) {
                            foreend.localPosition = foreend.localPosition + foreend.right * 0.1f;
                        }
                    }                    
                } 
            }   
        //}

        //Shoot();
    }

    private void Shoot()
    {
        
        Debug.Log("Shoot shotgun!");
        
        //Transform gunPoint = transform.GetChild(0);
        Vector3 leftRayDirection = gunPoint.forward + new Vector3(0f, 0f, 0.2f);
        Vector3 rightRayDirection = gunPoint.forward + new Vector3(0f, 0f, -0.2f);
        Vector3 topRayDirection = gunPoint.forward + new Vector3(0f, 0.2f, 0f);
        Vector3 bottomRayDirection = gunPoint.forward + new Vector3(0f, -0.2f, 0f);
        float rayLengthForward = 10f;
        float rayLengthOuter = 7.5f; //left,right,top, and bottom ray length
        int forwardDamage = 10;
        int outerDamage = 6;
        

        
        Debug.DrawLine(gunPoint.position, gunPoint.forward * rayLengthForward, Color.blue, 10);
        Debug.DrawLine(gunPoint.position, leftRayDirection * rayLengthOuter, Color.green, 10);
        Debug.DrawLine(gunPoint.position, rightRayDirection * rayLengthOuter, Color.red, 10);
        Debug.DrawLine(gunPoint.position, topRayDirection * rayLengthOuter, Color.yellow, 10);
        Debug.DrawLine(gunPoint.position, bottomRayDirection * rayLengthOuter, Color.magenta, 10);
        

        //forward shooting raycast line
        MakeShootingRaycast(gunPoint, gunPoint.forward, rayLengthForward, forwardDamage, Color.blue);

        //left shooting raycast line
        MakeShootingRaycast(gunPoint, leftRayDirection, rayLengthOuter, outerDamage, Color.green);

        //right shooting raycast line
        MakeShootingRaycast(gunPoint, rightRayDirection, rayLengthOuter, outerDamage, Color.red);

        //top shooting raycast line
        MakeShootingRaycast(gunPoint, topRayDirection, rayLengthOuter, outerDamage, Color.yellow);

        //bottom shooting raycast line
        MakeShootingRaycast(gunPoint, bottomRayDirection, rayLengthOuter, outerDamage, Color.magenta);
    }

    private void MakeShootingRaycast(Transform gunPoint, Vector3 rayDirection, float rayLength, int damage, Color c) {
        RaycastHit hit;
        Vector3 rayStart = gunPoint.position;
        Vector3 rayEndPoint = rayDirection * rayLength;

        if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength)) {
            Debug.Log(hit.collider.name);
            //access enemy and deal damage
            Collider collided = hit.collider;
            if (collided.CompareTag("Enemy") || collided.CompareTag("Neutral") || collided.CompareTag("Friendly")) {
                if (collided.GetComponent<Character>() != null) { 
                    collided.GetComponent<Character>().TakeDamage(damage);
                    //hit.collider.GetComponent<Character>().TakeDamage(damage);
                } 
            }

        }
        //Debug.DrawLine(rayStart, rayEndPoint, c);
    }

    public void OnIsActivated(bool activate) {
        IsActivated = activate;
        Debug.Log("Activate shotgun: " + activate);
    }

}
