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

    [Header("Drones")]
    [SerializeField] private GameObject dronePrefab = null;
    [SerializeField] private int droneSpawnPhase = 3;
    [SerializeField] private float droneSpawnInterval = 4f;
    [SerializeField] private float droneSpawnDecreasePerPhase = 0.6f;
    [SerializeField] private float droneSpawnMin = 1f;
    [SerializeField] private float droneSpawnDistanceFromMiddle = 4f;
    [SerializeField] private float droneSpawnRange = 1f;
    [SerializeField] private float droneMoveSpeed = 3f;

    [Header("Piss Missles")]
    [SerializeField] private GameObject pissMisslePrefab = null;
    [SerializeField] private int pissMissleSpawnPhase = 3;
    [SerializeField] private int threeFoldPissMisslesSpawnPhase = 5;
    [SerializeField] private int fiveFoldPissMisslesSpawnPhase = 6;
    [SerializeField] private float pissMissleSpawnInterval = 9f;
    [SerializeField] private float pissMissleSpawnDecreasePerPhase = 1f;
    [SerializeField] private float pissMissleSpawnMin = 4f;
    [SerializeField] public float pissMissleSpeed = 20f;
    [SerializeField] public float pissMissleLaunchDelay = 2f;
    [SerializeField] private float pissMissleLaunchDelayDecreasePerPhase = 0.2f;
    [SerializeField] private float pissMissleLaunchDelayMin = 0.5f;
    [SerializeField] private float pissMissleMultipleIncreaseAngle = 30f;

    [Header("Powerups")]
    [Tooltip("Powerups will appear some random time inbetween these two. x is min, y is max (seconds)")]
    [SerializeField] private Vector2 PowerupInvervalRange = new Vector2(15, 30);
    [SerializeField] private Vector2 PowerupSpeed = new Vector2(10, 20);
    [SerializeField] private List<GameObject> PowerupPrefabs;

    [Header("MSC.")]
    [SerializeField] private float spawnDistanceOffsetX = 10f;
    [SerializeField] private float spawnDistanceOffsetY = 6f;

    float asteroidTimeLeft = 0f;
    float bigAsteroidTimeLeft = 0f;
    float lazerTimeLeft = 5f;
    float droneTimeLeft = 2f;
    float pissMissleTimeLeft = 2f;
    float timeUntilNextPowerup = 15f;
    [HideInInspector] public bool lazerUp = false;
    public int phase = 1;
    [HideInInspector]public bool frozen = false;
    List<GameObject> frozenObjs = new List<GameObject>();
    List<Vector2> frozenObjsVelocity = new List<Vector2>();
    List<float> frozenObjsRotation = new List<float>();

    // Update is called once per frame
    void Update()
    {
        if (frozen)
        {
            return;
        }
        SpawnHazards();
        SpawnPowerups();
    }

    void SpawnPowerups()
    {
        timeUntilNextPowerup -= Time.deltaTime;
        if (timeUntilNextPowerup <= 0)
        {
            timeUntilNextPowerup = Random.Range(PowerupInvervalRange.x, PowerupInvervalRange.y);
            SpawnAnyPowerup();
        }
    }

    void SpawnAnyPowerup()
    {
        //When we add new powerups, we would randomly pick one to instantiate
        GameObject powerupPrefab = PowerupPrefabs[Random.Range(0, PowerupPrefabs.Count)];
        float side = Random.Range(1, 5);
        GameObject prefab;
        if (side == 1)
        {
            // spawn from the left side
            Vector2 spawnPosition = new Vector3(-spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(PowerupSpeed.x, PowerupSpeed.y), ForceMode2D.Impulse);
        }
        else if (side == 2)
        {
            // spawn from the right side
            Vector2 spawnPosition = new Vector3(spawnDistanceOffsetX, Random.Range(-spawnDistanceOffsetY, spawnDistanceOffsetY));
            prefab = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(PowerupSpeed.x, PowerupSpeed.y), ForceMode2D.Impulse);

        }
        else if (side == 3)
        {
            // spawn from the top side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), spawnDistanceOffsetY);
            prefab = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(PowerupSpeed.x, PowerupSpeed.y), ForceMode2D.Impulse);
        }
        else
        {
            // spawn from the bottom side
            Vector2 spawnPosition = new Vector3(Random.Range(-spawnDistanceOffsetX, spawnDistanceOffsetX), -spawnDistanceOffsetY);
            prefab = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            prefab.GetComponentInChildren<Rigidbody2D>().AddForce(spawnPosition.normalized * -Random.Range(PowerupSpeed.x, PowerupSpeed.y), ForceMode2D.Impulse);

        }
    }

    void SpawnHazards()
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

        //spawn drones
        if (phase >= droneSpawnPhase)
        {
            droneTimeLeft -= Time.deltaTime;
            if (droneTimeLeft < 0)
            {
                droneTimeLeft = droneSpawnInterval;
                spawnDrone();
            }
        }

        //spawn piss missles
        if (phase >= pissMissleSpawnPhase)
        {
            pissMissleTimeLeft -= Time.deltaTime;
            if (pissMissleTimeLeft <= 0)
            {
                pissMissleTimeLeft = pissMissleSpawnInterval;
                spawnPissMissle();
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

    void spawnPissMissle()
    {
        GameObject pissMissle = Instantiate(pissMisslePrefab, Vector2.zero, Quaternion.identity);
        float rotation = Random.Range(0, 360);
        pissMissle.transform.Rotate(0, 0, rotation);
        if (phase >= threeFoldPissMisslesSpawnPhase)
        {
            pissMissle = Instantiate(pissMisslePrefab, Vector2.zero, Quaternion.identity);
            pissMissle.transform.Rotate(0, 0, rotation + pissMissleMultipleIncreaseAngle);
            pissMissle = Instantiate(pissMisslePrefab, Vector2.zero, Quaternion.identity);
            pissMissle.transform.Rotate(0, 0, rotation - pissMissleMultipleIncreaseAngle);
        }
        if (phase >= fiveFoldPissMisslesSpawnPhase)
        {
            pissMissle = Instantiate(pissMisslePrefab, Vector2.zero, Quaternion.identity);
            pissMissle.transform.Rotate(0, 0, rotation + pissMissleMultipleIncreaseAngle * 2);
            pissMissle = Instantiate(pissMisslePrefab, Vector2.zero, Quaternion.identity);
            pissMissle.transform.Rotate(0, 0, rotation - pissMissleMultipleIncreaseAngle * 2);
        }

    }

    void spawnDrone()
    {
        int num = Random.Range(0, 2);
        int side = 1;
        if (num == 0)
        {
            side = -1;
        }

        GameObject refDrone = Instantiate(dronePrefab, new Vector2((droneSpawnDistanceFromMiddle + Random.Range(0, droneSpawnRange)) * side, spawnDistanceOffsetY), Quaternion.identity);
        refDrone.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(0, -droneMoveSpeed), ForceMode2D.Impulse);
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

        //decrease the lazer spawn rate
        if (phase >= lazerSpawnPhase + 1)
        {
            lazerDowntime -= lazerDowntimeDecreasePerPhase;
            lazerUptime += lazerUptimeIncreasePerPhase;
            if (lazerDowntime < lazerDowntimeMin)
            {
                lazerDowntime = lazerDowntimeMin;
            }

        }

        //decrease the big asteroid stuff
        if (phase >= bigAsteroidSpawnPhase + 1)
        {
            bigAsteroidSpawnInterval -= bigAsteroidSspawnDecreasePerPhase;
            if (bigAsteroidSpawnInterval < bigAsteroidSpawnMin)
            {
                bigAsteroidSpawnInterval = bigAsteroidSpawnMin;
            }
        }

        //decrease the drone stuff
        if (phase >= droneSpawnPhase + 1)
        {
            droneSpawnInterval -= droneSpawnDecreasePerPhase;
            if (droneSpawnInterval < droneSpawnMin)
            {
                droneSpawnInterval = droneSpawnMin;
            }
        }

        if (phase > pissMissleSpawnPhase + 1)
        {
            pissMissleSpawnInterval -= pissMissleSpawnDecreasePerPhase;
            if (pissMissleSpawnInterval < pissMissleSpawnMin)
            {
                pissMissleSpawnInterval = pissMissleSpawnMin;
            }
            pissMissleLaunchDelay -= pissMissleLaunchDelayDecreasePerPhase;
            if (pissMissleLaunchDelay < pissMissleLaunchDelayMin)
            {
                pissMissleLaunchDelay = pissMissleLaunchDelayMin;
            }
        }
    }

    public void freeze()
    {
        frozen = true;
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.CompareTag("Destroyable") || obj.CompareTag("SpeedPowerup") || obj.CompareTag("NukePowerup") || obj.CompareTag("ShieldPowerup") || obj.CompareTag("GIANTFUCKOFFLAZER"))
            {
                frozenObjs.Add(obj);
                Rigidbody2D rb = obj.GetComponentInChildren<Rigidbody2D>();
                frozenObjsVelocity.Add(rb.velocity);
                frozenObjsRotation.Add(rb.angularVelocity);
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
            } 
        }
    }

    public void thaw()
    {
        frozen = false;
        for (int i = 0; i < frozenObjs.Count; i++)
        {
            try
            {


                GameObject obj = frozenObjs[i];
                Rigidbody2D rb = obj.GetComponentInChildren<Rigidbody2D>();
                rb.AddForce(frozenObjsVelocity[i], ForceMode2D.Impulse);
                rb.AddTorque(frozenObjsRotation[i]);
            }
            catch
            {
                try
                {
                    Destroy(frozenObjs[i]);
                }
                catch
                {

                }
            }
        }
        frozenObjs = new List<GameObject>();
        frozenObjsVelocity = new List<Vector2>();
        frozenObjsRotation = new List<float>();
    }

}
