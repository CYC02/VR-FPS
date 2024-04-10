using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j_spawn_ammo : MonoBehaviour
{
    public GameObject ammoBox;

    private bool spawnedAmmo = false;
    private float ammoSpawnRate = 10f;
    private float nextSpawnAttempt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnAmmoRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnedAmmo)
        {
            if (Time.time > nextSpawnAttempt)
            {
                nextSpawnAttempt = Time.time + ammoSpawnRate;
                SpawnAmmoRandomly();
                nextSpawnAttempt = 0f;
            }
        }
    }

    void SpawnAmmoRandomly()
    {
        int randomizeNumber = Random.Range(0, 20);
        int chosenRandomNumber = Random.Range(0, 20);
        // Debug.Log(randomizeNumber);
        // Debug.Log(chosenRandomNumber);

        if (randomizeNumber == chosenRandomNumber)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(ammoBox, position, Quaternion.identity);
            spawnedAmmo = true;
            // Debug.Log("Spawned");
        }
        else
        {
            spawnedAmmo = false;
        }
    }
}
