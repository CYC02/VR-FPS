using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class j_weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_spawn_area;

    public XRSocketInteractor ammo_socket;

    float bullet_force = 8f;

    int ammo_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        ammo_socket.onSelectEntered.AddListener(LoadWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadWeapon(XRBaseInteractable ammo)
    {
        ammo_count += 1;
        
        Destroy(ammo.gameObject);
        Debug.Log(ammo_count);
    }

    public void ShootWeapon()
    {
        if (ammo_count > 0)
        {
            var position = new Vector3(bullet_spawn_area.position.x, bullet_spawn_area.position.y, bullet_spawn_area.position.z);
            var b = Instantiate(bullet, position, Quaternion.identity);
            var b_rigidbody = b.GetComponent<Rigidbody>();
            b_rigidbody.AddForce(bullet_spawn_area.forward * bullet_force, ForceMode.Impulse);
            ammo_count -= 1;
        }

    }  
}
