using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class j_weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_spawn_area;

    Rigidbody bullet_body;

    // Start is called before the first frame update
    void Start()
    {
        bullet_body = bullet.GetComponent<Rigidbody>();
        // Transform[] transforms = GetComponents<Transform>();
        // Debug.Log(transforms);
    }

    // Update is called once per frame
    void Update()
    {
        ShootWeapon();
    }

    void ShootWeapon()
    {
        var position = new Vector3(bullet_spawn_area.position.x, bullet_spawn_area.position.y, bullet_spawn_area.position.z);
        Instantiate(bullet, position, Quaternion.identity);
        bullet_body.AddForce(bullet_spawn_area.forward, ForceMode.Impulse);
    }  
}
