using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

//Author: Cindy Chan
//Manages the Camera canvas to view the surveillance cameras

public class CamerasCanvas : MonoBehaviour
{
    InputInfo inputInfo;
    public GameObject[] cam;
    private int camIdx = 0;

    private int pressedPrimaryCounter = 0;
    private int pressedSecondaryCounter = 0;

    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        if (inputInfo == null) {
            inputInfo = GetComponent<InputInfo>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool primaryButtonDown = false;
        bool secondaryButtonDown = false;
        if (gameObject.activeSelf) {
            //go to next camera view
            if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonDown) && primaryButtonDown)
            {
                if (pressedPrimaryCounter == 0)
                {
                    if (cam != null && cam.Length != 0)
                    {
                        cam[camIdx].SetActive(false);

                        if (camIdx == cam.Length - 1)
                        {
                            //last index. go back to first index                       
                            camIdx = 0;
                        }
                        else
                        {
                            camIdx++;
                        }
                        cam[camIdx].SetActive(true);
                        textMesh.SetText("Camera" + (camIdx + 1).ToString());
                    }
                    pressedPrimaryCounter = 1;
                }

            }
            else {
                pressedPrimaryCounter= 0;
            }
            //go back to previous camera view
            if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonDown) && secondaryButtonDown)
            {
                if (pressedSecondaryCounter == 0)
                {
                    if (cam != null && cam.Length != 0)
                    {
                        cam[camIdx].SetActive(false);

                        if (camIdx == 0)
                        {
                            //first index becomes last
                            camIdx = cam.Length - 1;
                        }
                        else
                        {
                            camIdx--;
                        }
                        cam[camIdx].SetActive(true);
                        textMesh.SetText("Camera " + (camIdx + 1).ToString());
                    }
                    pressedSecondaryCounter = 1;
                }
            }
            else {
                pressedSecondaryCounter= 0;
            }
    }
    }
}
