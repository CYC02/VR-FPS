using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class j_weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_spawn_area;
    public XRSocketInteractor ammo_socket;
    public GameObject ammo_textfield;

    float bullet_force = 20f;
    int ammo_count = 10;

    TextMeshProUGUI ammo_text;

    // Start is called before the first frame update
    void Start()
    {
        ammo_socket.onSelectEntered.AddListener(LoadWeapon);
        ammo_text = ammo_textfield.GetComponent<TextMeshProUGUI>();
    }
    public void LoadWeapon(XRBaseInteractable ammo)
    {
        ammo_count += 1;
        Destroy(ammo.gameObject);

        updateAmmoText();
    }

    public void ShootWeapon()
    {
        if (ammo_count > 0)
        {
            var position = new Vector3(bullet_spawn_area.position.x, bullet_spawn_area.position.y, bullet_spawn_area.position.z);
            var b = Instantiate(bullet, position, bullet_spawn_area.rotation);
            var b_rigidbody = b.GetComponent<Rigidbody>();
            b_rigidbody.AddForce(bullet_spawn_area.forward * bullet_force, ForceMode.Impulse);
            ammo_count -= 1;

            updateAmmoText();
        }
    }  
    void updateAmmoText()
    {
        var ammo_str = ammo_text.text;
        var ammo_index = ammo_str.IndexOf('B');
        var ammo_substr = ammo_str.Substring(0, ammo_index + 2);
        ammo_substr += ammo_count.ToString();
        ammo_text.text = ammo_substr;
    }
}
