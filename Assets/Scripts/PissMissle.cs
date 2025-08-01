using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PissMissle : MonoBehaviour
{
    float speed = 10f;
    float delay = 2f;

    [SerializeField] AudioSource telegraphSFX, flythrough;

    private void Start()
    {
        speed = FindObjectOfType<ObstacleGenerator>().pissMissleSpeed;
        delay = FindObjectOfType<ObstacleGenerator>().pissMissleLaunchDelay;
        telegraphSFX.Play();
        StartCoroutine(delayLaunch());
    }

    IEnumerator delayLaunch()
    {
        yield return new WaitForSeconds(delay);
        flythrough.Play();
        GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * -speed, ForceMode2D.Impulse);
    }
}
