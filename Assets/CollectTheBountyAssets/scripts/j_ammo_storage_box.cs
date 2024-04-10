using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class j_ammo_storage_box : MonoBehaviour
{
    // public GameObject bullet;
    // public Transform bullet_supply_area;
    // public XRSocketInteractor ammo_socket;
    // public XRSocketInteractor ammo_supply_socket;

    // int ammo_extra_count = 1;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     ammo_socket.onSelectEntered.AddListener(StoreAmmo);
    //     ammo_supply_socket.onSelectExited.AddListener(SupplyAmmo);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // public void StoreAmmo(XRBaseInteractable ammo)
    // {
    //     ammo_extra_count += 1;
    //     Destroy(ammo.gameObject);
    //     // if (!ammo_supply_socket.hasSelection)
    //     // {
    //     //     SupplyAmmo(ammo);
    //     // } else {
    //     //     Destroy(ammo.gameObject);
    //     // }
    // }

    // public void SupplyAmmo(XRBaseInteractable ammo)
    // {
    //     if (ammo_extra_count > 0 && !ammo_supply_socket.hasSelection)
    //     {
    //         ammo_extra_count -= 1;
    //         var position = new Vector3(bullet_supply_area.position.x, bullet_supply_area.position.y, bullet_supply_area.position.z);
    //         Instantiate(bullet, position, Quaternion.identity);
    //         // ammo.gameObject.position = position;
    //     }
    // }
}
