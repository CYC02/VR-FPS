using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class j_weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_spawn_area;

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
        Instantiate(bullet, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(0f,0f,1f, ForceMode.Impulse);
    }  
}
