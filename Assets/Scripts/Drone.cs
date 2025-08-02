using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Drone : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int shots = 4;
    [Tooltip("where x is the minimum time and y is the maximum time (seconds)")]
    [SerializeField] Vector2 interval = new Vector2(1, 2);
    [SerializeField] float projectileSpeed;
    [SerializeField] AudioSource shootSFX;

    float timeUntilShot = 0;
    float nextShotInterval;
    GameObject player = null;

    private void Start()
    {
        timeUntilShot = interval.y;
        nextShotInterval = Random.Range(interval.x, interval.y);
        if (FindObjectOfType<Player>() == null)
        {
            Debug.LogError("no player found!");
            return;
        }
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        //face the player
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        if (shots <= 0)
        {
            return;
        }
        if (player == null)
        {
            return;
        }
        if (FindObjectOfType<ObstacleGenerator>().frozen)
        {
            return;
        }
        timeUntilShot -= Time.deltaTime;
        if (timeUntilShot <= 0)
        {
            timeUntilShot = Random.Range(interval.x, interval.y);
            shots--;
            GameObject refProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            //look at the player
            Vector2 direction = player.transform.position - refProjectile.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            refProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            //fire!!!
            refProjectile.GetComponentInChildren<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
            shootSFX.Play();
        }
    }
}
