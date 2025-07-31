using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("Asteroid")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float asteroidSpawnInterval = 2.0f;
    [SerializeField] private float asteroidSspawnDecreasePerPhase = 0.4f;
    [SerializeField] private float asteroidSpawnMin = 0.8f;
    [SerializeField] private float asteroidRotateAmount = 20f;
    [SerializeField] private float asteroidSpeedMin = 10f;
    [SerializeField] private float asteroidSpeedMax = 20f;

    [Header("Lazer")]
    [SerializeField] private GameObject lazerPrefab;
    [SerializeField] private int lazerSpawnPhase = 2;
    [SerializeField] private float lazerDowntime = 15;
    [SerializeField] public float lazerUptime = 10;
    [SerializeField] private float lazerDowntimeMin = 5f;
    [SerializeField] private float lazerDowntimeDecreasePerPhase = 2f;
    [SerializeField] private float lazerUptimeIncreasePerPhase = 4f;
    [Header("Big Asteroids")]
    [SerializeField] private GameObject bigAsteroidPrefab;
    [SerializeField] private int bigAsteroidSpawnPhase = 3;
    [SerializeField] private float bigAsteroidSpawnInterval = 2.0f;
    [SerializeField] private float bigAsteroidSspawnDecreasePerPhase = 0.4f;
    [SerializeField] private float bigAsteroidSpawnMin = 0.8f;
    [SerializeField] private float bigAsteroidRotateAmount = 20f;
    [SerializeField] private float bigAsteroidSpeedMin = 10f;
    [SerializeField] private float bigAsteroidSpeedMax = 20f;


    [Header("MSC.")]
    [SerializeField] private float spawnDistanceOffsetX = 10f;
    [SerializeField] private float spawnDistanceOffsetY = 6f;

    float asteroidTimeLeft = 0f;
    float bigAsteroidTimeLeft = 0f;
    float lazerTimeLeft = 5f;
    public bool lazerUp = false;
    public int phase = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //spawn Asteroid Code
        asteroidTimeLeft -= Time.deltaTime;
        if (asteroidTimeLeft < 0)
        {
            //spawn asteroid
            SpawnAsteroid();
            asteroidTimeLeft = asteroidSpawnInterval;
        }

        //spawn lazer code
        if (phase >= lazerSpawnPhase && lazerUp == false)
        {
            lazerTimeLeft -= Time.deltaTime;
            if (lazerTimeLeft < 0)
            {
                lazerTimeLeft = lazerDowntime;
                lazerUp = true;
                spawnLazer();
            }
        }

        //spawn Beeg asteroids
        if (phase >= bigAsteroidSpawnPhase)
        {
            bigAsteroidTimeLeft -= Time.deltaTime;
            if (bigAsteroidTimeLeft < 0)
            {
                bigAsteroidTimeLeft = bigAsteroidSpawnInterval;
                SpawnBigAsteroid();
            }
        }
    }

    void SpawnBigAsteroid()
    {
        float side = Random.Range(1, 5);
        GameObject prefab = null;
        if (side == 1)
        {
            // spawn from the left side
            Vector2 spawnPosition = new Vector3(-spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(bigAsteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(bigAsteroidSpeedMin, bigAsteroidSpeedMax), ForceMode2D.Impulse);
        }
        else if (side == 2)
        {
            // spawn from the right side
            Vector2 spawnPosition = new Vector3(spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(bigAsteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(bigAsteroidSpeedMin, bigAsteroidSpeedMax), ForceMode2D.Impulse);

        }
        else if (side == 3)
        {
            // spawn from the top side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), spawnDistanceOffsetY);
            prefab = Instantiate(bigAsteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(bigAsteroidSpeedMin, bigAsteroidSpeedMax), ForceMode2D.Impulse);
        }
        else
        {
            // spawn from the bottom side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), -spawnDistanceOffsetY);
            prefab = Instantiate(bigAsteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(bigAsteroidSpeedMin, bigAsteroidSpeedMax), ForceMode2D.Impulse);

        }
        prefab.GetComponentInChildren<Rigidbody2D>().AddTorque(Random.Range(-bigAsteroidRotateAmount, bigAsteroidRotateAmount));
    }

    void SpawnAsteroid()
    {
        float side = Random.Range(1, 5);
        GameObject prefab = null;
        if (side == 1)
        {
            // spawn from the left side
            Vector2 spawnPosition = new Vector3(-spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(asteroidSpeedMin, asteroidSpeedMax), ForceMode2D.Impulse);
        }
        else if (side == 2)
        {
            // spawn from the right side
            Vector2 spawnPosition = new Vector3(spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(asteroidSpeedMin, asteroidSpeedMax), ForceMode2D.Impulse);

        }
        else if (side == 3)
        {
            // spawn from the top side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), spawnDistanceOffsetY);
            prefab = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(asteroidSpeedMin, asteroidSpeedMax), ForceMode2D.Impulse);
        }
        else
        {
            // spawn from the bottom side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), -spawnDistanceOffsetY);
            prefab = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(asteroidSpeedMin, asteroidSpeedMax), ForceMode2D.Impulse);

        }
        prefab.GetComponentInChildren<Rigidbody2D>().AddTorque(Random.Range(-asteroidRotateAmount, asteroidRotateAmount));
    }
    void spawnLazer()
    {
        GameObject lazer = Instantiate(lazerPrefab, Vector2.zero, Quaternion.identity);
        lazer.transform.Rotate(0, 0, Random.Range(0, 360));
    }

    public void RaisePhase()
    {
        phase++;

        //decrease the asteroid spawn rate
        asteroidSpawnInterval -= asteroidSspawnDecreasePerPhase;
        if (asteroidSpawnInterval < asteroidSpawnMin)
        {
            asteroidSpawnInterval = asteroidSpawnMin;
        }
        ;
        //decrease the lazer spawn rate
        if (phase + 1 > lazerSpawnPhase)
        {
            lazerDowntime -= lazerDowntimeDecreasePerPhase;
            lazerUptime += lazerUptimeIncreasePerPhase;
            if (lazerDowntime < lazerDowntimeMin)
            {
                lazerDowntime = lazerDowntimeMin;
            }

        }

        //decrease the big asteroid stuff
        if (phase + 1 > bigAsteroidSpawnPhase)
        {
            bigAsteroidSpawnInterval -= bigAsteroidSspawnDecreasePerPhase;
            if (bigAsteroidSpawnInterval < bigAsteroidSpawnMin)
            {
                bigAsteroidSpawnInterval = bigAsteroidSpawnMin;
            }
        }
    }

}
