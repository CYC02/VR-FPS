using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

//reference: https://www.youtube.com/watch?v=j9F6kEB9rBg
//reference: https://www.youtube.com/watch?v=1gPLfY93JHk
public class Shotgun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> m_device = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, m_device);
        
        if (m_device.Count == 1) {
            InputDevice device = m_device[0];
            //One right side device found
            bool triggerButtonDown = false;
            bool gripButtonDown = false;
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonDown) && triggerButtonDown) {
                Debug.Log("Right trigger button pressed");

                //if gripping the gun and pressed the trigger button, then shoot
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonDown) && gripButtonDown) {
                    Shoot();
                }
                
            }
            
        }
        Shoot();
    }

    public void Shoot()
    {
        Debug.Log("Shoot shotgun!");
        
        Transform gunPoint = transform.GetChild(0);
        Vector3 leftRayDirection = gunPoint.forward + new Vector3(0f, 0f, 0.2f);
        Vector3 rightRayDirection = gunPoint.forward + new Vector3(0f, 0f, -0.2f);
        Vector3 topRayDirection = gunPoint.forward + new Vector3(0f, 0.2f, 0f);
        Vector3 bottomRayDirection = gunPoint.forward + new Vector3(0f, -0.2f, 0f);
        float rayLengthForward = 10f;
        float rayLengthOuter = 7.5f;
        int forwardDamage = 10;
        int outerDamage = 6;

        
        Debug.DrawLine(gunPoint.position, gunPoint.forward * rayLengthForward, Color.blue);
        Debug.DrawLine(gunPoint.position, leftRayDirection * rayLengthOuter, Color.green);
        Debug.DrawLine(gunPoint.position, rightRayDirection * rayLengthOuter, Color.red);
        Debug.DrawLine(gunPoint.position, topRayDirection * rayLengthOuter, Color.yellow);
        Debug.DrawLine(gunPoint.position, bottomRayDirection * rayLengthOuter, Color.magenta);
        


        /*
        RaycastHit hit;
        Vector3 rayStart = gunPoint.position;
        Vector3 rayDirection = gunPoint.forward;
        float rayLength = 100f;
        Vector3 rayEndPoint = gunPoint.forward * rayLength;

        if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength)) {
            Debug.Log(hit.collider.name);
        }
        Debug.DrawLine(rayStart, rayEndPoint, Color.green, 10);
        */

        //forward shooting raycast line
        MakeShootingRaycast(gunPoint, gunPoint.forward, rayLengthForward, forwardDamage);

        //left shooting raycast line
        MakeShootingRaycast(gunPoint, leftRayDirection, rayLengthOuter, outerDamage);

        //right shooting raycast line
        MakeShootingRaycast(gunPoint, rightRayDirection, rayLengthOuter, outerDamage);

        //top shooting raycast line
        MakeShootingRaycast(gunPoint, topRayDirection, rayLengthOuter, outerDamage);

        //bottom shooting raycast line
        MakeShootingRaycast(gunPoint, bottomRayDirection, rayLengthOuter, outerDamage);
    }

    private void MakeShootingRaycast(Transform gunPoint, Vector3 rayDirection, float rayLength, int damage) {
        RaycastHit hit;
        Vector3 rayStart = gunPoint.position;
        //Vector3 rayEndPoint = rayDirection * rayLength;

        if (Physics.Raycast(rayStart, rayDirection, out hit, rayLength)) {
            Debug.Log(hit.collider.name);
            //access enemy and deal damage
        }
    }
}
