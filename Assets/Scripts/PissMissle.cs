using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PissMissle : MonoBehaviour
{
    float speed = 10f;
    float delay = 2f;

    private void Start()
    {
        speed = FindObjectOfType<ObstacleGenerator>().pissMissleSpeed;
        delay = FindObjectOfType<ObstacleGenerator>().pissMissleLaunchDelay;
        StartCoroutine(delayLaunch());
    }

    IEnumerator delayLaunch()
    {
        yield return new WaitForSeconds(delay);

        GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * -speed, ForceMode2D.Impulse);
    }
}
