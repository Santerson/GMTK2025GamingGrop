using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuSpawner : MonoBehaviour
{
    [SerializeField] float spawnrate = 1f;
    [SerializeField] GameObject spawnObj;

    float timeUntilSpawn = 0f;

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            timeUntilSpawn = spawnrate;
            GameObject prefab;
            Vector2 spawnPosition = new Vector3(Random.Range(-9, 9), 5);
            prefab = Instantiate(spawnObj, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(Vector2.up * -Random.Range(1, 2), ForceMode2D.Impulse);
            prefab.GetComponentInChildren<Rigidbody2D>().AddTorque(Random.Range(-20, 20));

        }
    }
}
