using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
public class Shotgun : InteractableResource
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Transform foreend;
    private bool IsActivated = false; //shotgun activation
    private bool IsHoldingSlugAmmo = false;
    //private XRGrabInteractable grabInteractable;
    private InputInfo inputInfo;

    private Vector3 foreendUp;
    private Vector3 foreendDown;
    private bool isForeendUp;
    private float foreendCooldownDuration = 0.2f;
    private float foreendCooldownTimer = 0f;

    [SerializeField] private GameObject slugDropZone;
    private int slugAmmoLoadedInGun = 0;
    [SerializeField] private int maxSlugAmmoLoadedInGun = 4;

    [SerializeField] private GameObject extractedAmmoPrefab;
    [SerializeField] private Transform extractedAmmoLocation;
    private int usedSlugAmmoCaseInGun = 0;

    [SerializeField] private ParticleSystem muzzleExplosion;
    [SerializeField] private ParticleSystem muzzleSmoke;
    [SerializeField] private Transform muzzleLocation;

    private bool isSelectedByLeftHand = false;


    private bool hasMadeAShot = false;

    // Start is called before the first frame update

    void Start()
    {
        inputInfo = GetComponent<InputInfo>();

        foreendUp = foreend.localPosition + foreend.forward * 0.1f;
        foreendDown = foreend.localPosition;
        isForeendUp= false;

    }

    void Update()
    {
        bool rightTriggerButtonDown = false;
        bool rightGripButtonDown = false;

        bool leftGripButtonDown = false;

        if (foreendCooldownTimer > 0f)
        {
            foreendCooldownTimer -= Time.deltaTime;
        }
        else {
            foreendCooldownTimer = 0f;
        }

        //if gripping the gun and pressed the trigger button, then shoot
        if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.gripButton, out rightGripButtonDown) && rightGripButtonDown)
        //if (device.TryGetFeatureValue(CommonUsages.gripButton, out rightGripButtonDown) && rightGripButtonDown)
        {
            //Debug.Log("Grip button pressed");
            //ensures that the shooting only occurs when activating/holding the shotgun
            if (IsActivated)
            {
                if (hasMadeAShot == false)
                {
                    if (inputInfo.rightController.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerButtonDown) && rightTriggerButtonDown)
                    //if (device.TryGetFeatureValue(CommonUsages.triggerButton, out rightTriggerButtonDown) && rightTriggerButtonDown)
                    {
                        Debug.Log("Right trigger button pressed");

                        if (usedSlugAmmoCaseInGun == 0 && slugAmmoLoadedInGun > 0)
                        {
                            Shoot();
                        }
                        else
                        {
                            Debug.Log("Must extracted the used bullet casing in the gun before the player can shoot.");
                        }
                    }
                }
            }
            //extracting bullet casing
            //ensure left hand grip is true
            if (inputInfo.leftController.TryGetFeatureValue(CommonUsages.gripButton, out leftGripButtonDown) && leftGripButtonDown)
            {
                //get velocity of left hand controller
                if (inputInfo.leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
                {
                    //if the left hand passes a certain forward velocity, move the fore-end up
                    //if the velocity passes a certain velocity backwards, move the fore-end down to the original position
                    float leftMag = leftVelocity.magnitude;
                    //Debug.Log(leftMag);
                    if (foreendCooldownTimer == 0 && isSelectedByLeftHand)
                    {
                        if (isForeendUp != true && leftMag > 1f)
                        {
                            SwitchForeendPosition(true);
                        }
                        else
                        {
                            if (leftMag > 0.7f)
                            {
                                SwitchForeendPosition(false);
                                ExtractSlugAmmo();
                            }
                        }
                        foreendCooldownTimer = foreendCooldownDuration;
                    }
                }

            }
            else
            {
                SwitchForeendPosition(false);
            }
        }
        else { 
            SwitchForeendPosition(false);
        }
    }

    /*
     * SHOTGUN ACTIVATION AND SHOOTING
     */
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

        //play particle effects
        PlayShootingParticleEffects();

        //play shooting audio
        PlayShootingAudio();

        //Make empty ammo casing
        usedSlugAmmoCaseInGun++;

        hasMadeAShot = true;

        //recoil
        MakeRecoil(inputInfo.rightController);
        if (isSelectedByLeftHand) {
            MakeRecoil(inputInfo.leftController);
        }
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

    //Event triggers this function which determines if the Shotgun is activated(picked up) or deactivated (dropped from hand)
    public void OnIsActivated(bool activate) {
        IsActivated = activate;
        Debug.Log("Activate shotgun: " + activate);
        if (!activate) {
            SwitchForeendPosition(false);
        }
    }

    /*
     * CHECK AND CHECK IF SELECTING SHOTGUN WITH LEFT HAND DIRECT INTERACTOR
     */

    //Function is called by the Left Hand Direct Interactor. Selected by left hand.
    public void OnSelectedWithLeftHand() {
        isSelectedByLeftHand= true;
    }

    //Function is called by the Left Hand Direct Interactor. Not selected by left hand.
    public void OnSelectedExitedWithLeftHand()
    {
        isSelectedByLeftHand = false;
    }

    /*
     * FORE-END AND EXTRACTING AMMO
     */

    //Switched the foreend position on the gun in the up position or down
    //If switched upwards, then extract bulltet casing
    private void SwitchForeendPosition(bool up) {
        if (up)
        {
            foreend.localPosition = foreendUp;
            PlayForeEndAudio();
        }
        else {
            foreend.localPosition = foreendDown;
        }
        isForeendUp= up;
    }

    //Instaniates extracted bullet casing when foreend is at the down position
    private void ExtractSlugAmmo() {
        if (usedSlugAmmoCaseInGun == 0)
        {
            //Debug.Log("No used slug ammo casing to extract");
        }
        else if (usedSlugAmmoCaseInGun == 1)
        {
            usedSlugAmmoCaseInGun -= 1;
            slugAmmoLoadedInGun -= 1;
            XRSocketInteractor dropZoneSocket = slugDropZone.GetComponent<XRSocketInteractor>();
            dropZoneSocket.socketActive= true;
            PlayLoadingAmmoAudio();
            GameObject extractedAmmoInstance = Instantiate(extractedAmmoPrefab, extractedAmmoLocation.position, Quaternion.identity);
            hasMadeAShot = false;
        }
        else {
            Debug.LogError("Invalid number for usedSlugAmmoCaseInGun");
        }
    }

    /*
     * SLUG AMMO DROP ZONE
     */

    //Event triggers this function which hides the mesh for the slug drop zone and registers that a slug is inside the drop zone
    public void OnSelectEnteredAmmoDropZone() {
        ShowSlugDropZoneMesh(false);
            
        XRSocketInteractor dropZoneSocket = slugDropZone.GetComponent<XRSocketInteractor>();
        if (dropZoneSocket != null)
        {
            IXRSelectInteractable interactable = dropZoneSocket.firstInteractableSelected;
            if (interactable != null) {
                if (slugAmmoLoadedInGun < maxSlugAmmoLoadedInGun)
                {
                    slugAmmoLoadedInGun += 1;
                    PlayLoadingAmmoAudio();
                    Destroy(interactable.transform.gameObject);
                }
                else {
                    Debug.Log("Cannot load any slugs. Reached maximum slugs loaded into the gun");
                }
            }
        }
        else {
            Debug.LogError("Drop zone socket is null");
        }
        
    }

    //Event triggers this function which checks if the shotgun is fully loaded, if it is fully loaded, the socket is not active
    public void OnHoverEnteredAmmoDropZone() {
        XRSocketInteractor dropZoneSocket = slugDropZone.GetComponent<XRSocketInteractor>();
        if (slugAmmoLoadedInGun == maxSlugAmmoLoadedInGun)
        {

            if (dropZoneSocket != null)
            {
                dropZoneSocket.socketActive = false;
            }
            else
            {
                Debug.LogError("Drop zone socket is null");
            }
        }
        else if (slugAmmoLoadedInGun < maxSlugAmmoLoadedInGun)
        {
            if (dropZoneSocket != null)
            {
                dropZoneSocket.socketActive = true;
            }
            else
            {
                Debug.LogError("Drop zone socket is null");
            }
        }
        else {
            Debug.LogError("The amount of loaded slug ammo should not be greater than the maximum.");
        }
    }

    //Enable or disable the mesh of the slug drop zone
    public void ShowSlugDropZoneMesh(bool on) {
        MeshRenderer dropZoneMeshRender = slugDropZone.GetComponent<MeshRenderer>();
        if (dropZoneMeshRender != null)
        {
            dropZoneMeshRender.enabled = on;
        }
        else {
            Debug.LogError("Slug Drop Zone is null");
        }
    }

    /*
     * SHOTGUN PARTICLE EFFECTS
     */

    //Play Shooting particle effects
    private void PlayShootingParticleEffects() {
        if (muzzleExplosion != null && muzzleSmoke != null) {
            Instantiate(muzzleExplosion, muzzleLocation.position, Quaternion.identity);
            Instantiate(muzzleSmoke, muzzleLocation.position, Quaternion.identity);
        }
    }

    /*
     * SHOTGUN AUDIO
     */

    //Play Shooting audio
    private void PlayShootingAudio() {
        AudioSource audioSource = muzzleLocation.GetComponent<AudioSource>();
        if (audioSource != null) { 
            audioSource.Play();
        }
    }

    //Play loading ammo audio
    private void PlayLoadingAmmoAudio() { 
        AudioSource audioSource = slugDropZone.GetComponent<AudioSource>();
        if (audioSource != null) {
            audioSource.Play();
        }
    }

    //Play fore end audio when the fore end is in the up position
    private void PlayForeEndAudio() {
        AudioSource audioSource = foreend.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    /*
     * SHOTGUN RECOIL/ DEVICE HAPTICS
     */
    private void MakeRecoil(InputDevice device) {
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities)) {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = 0.5f;
                float duration = 1.0f;
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }
}
