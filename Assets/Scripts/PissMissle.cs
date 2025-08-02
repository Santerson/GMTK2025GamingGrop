using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class PissMissle : MonoBehaviour
{
    float speed = 10f;
    float delay = 2f;
    bool launched = false;

    [SerializeField] AudioSource telegraphSFX, flythrough;
    

    private void Start()
    {
        
        speed = FindObjectOfType<ObstacleGenerator>().pissMissleSpeed;
        delay = FindObjectOfType<ObstacleGenerator>().pissMissleLaunchDelay;
        telegraphSFX.Play();
        StartCoroutine(delayLaunch());
    }

    private void Update()
    {
        if (FindObjectOfType<ObstacleGenerator>().frozen)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (GetComponent<Rigidbody2D>().velocity == Vector2.zero && launched)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * -speed, ForceMode2D.Impulse);
        }
    }

    IEnumerator delayLaunch()
    {
        yield return new WaitForSeconds(delay);
        flythrough.Play();
        launched = true;
        GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * -speed, ForceMode2D.Impulse);
    }
}
