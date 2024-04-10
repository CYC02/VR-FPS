using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class j_weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_spawn_area;

    float bullet_force = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootWeapon()
    {
        var position = new Vector3(bullet_spawn_area.position.x, bullet_spawn_area.position.y, bullet_spawn_area.position.z);
        var b = Instantiate(bullet, position, Quaternion.identity);
        var b_rigidbody = b.GetComponent<Rigidbody>();
        b_rigidbody.AddForce(bullet_spawn_area.forward * bullet_force, ForceMode.Impulse);
    }  
}
