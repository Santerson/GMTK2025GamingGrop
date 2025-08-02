using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    [SerializeField] GameObject farLeftLine;
    [SerializeField] GameObject lessLeftLine;
    [SerializeField] GameObject lessRightLine;
    [SerializeField] GameObject farRightLine;
    [SerializeField] GameObject rotateaxis;

    [SerializeField] float initSpeed = 5;
    [SerializeField] float decayRate = 10;

    [SerializeField] AudioSource lazerWindupSFX;
    [SerializeField] AudioSource lazerUpSFX;


    float refScale = 0f;
    bool isLazerUp = false;
    BoxCollider2D refCollider = null;
    float upTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        upTime = FindObjectOfType<ObstacleGenerator>().lazerUptime;
        refCollider = GetComponent<BoxCollider2D>();
        lazerWindupSFX.Play();
    }

    float farLeftNet = 0;
    float farRightNet = 0;
    float lessLeftNet = 0;
    float lessRightNet = 0;
    //Update is called once per frame
    void FixedUpdate()
    {
        if (FindObjectOfType<ObstacleGenerator>().frozen)
        {
            return;
        }
        if (isLazerUp)
        {
            refCollider.enabled = true;
            upTime -= Time.deltaTime;
            if (upTime <= 0f)
            {
                FindObjectOfType<ObstacleGenerator>().lazerUp = false;
                Destroy(transform.parent.gameObject);
            }
        }
        else
        {
            setUpLazer();
        }
    }

    void setUpLazer()
    {
        if (FindObjectOfType<ObstacleGenerator>().frozen)
        {
            return;
        }
        if (farLeftNet < 90)
        {
            float angleStep = Mathf.Exp(-farLeftNet / decayRate) * initSpeed;

            farLeftLine.transform.RotateAround(rotateaxis.transform.position, Vector3.forward, angleStep);

            farLeftNet += angleStep;
        }
        if (farRightNet < 90)
        {
            float angleStep = Mathf.Exp(-farRightNet / decayRate) * initSpeed;

            farRightLine.transform.RotateAround(rotateaxis.transform.position, Vector3.forward, -angleStep);

            farRightNet += angleStep;
        }
        if (lessLeftNet < 180)
        {
            float angleStep = Mathf.Exp(-lessLeftNet / decayRate / 2) * initSpeed * 2;

            lessLeftLine.transform.RotateAround(rotateaxis.transform.position, Vector3.forward, angleStep);

            lessLeftNet += angleStep;

        }
        if (lessRightNet < 180)
        {
            float angleStep = Mathf.Exp(-lessRightNet / decayRate / 2) * initSpeed * 2;

            lessRightLine.transform.RotateAround(rotateaxis.transform.position, Vector3.forward, -angleStep);

            lessRightNet += angleStep;

        }
        if (farLeftNet >= 90 && farRightNet >= 90 && lessLeftNet >= 180 && lessRightNet >= 180)
        {
            isLazerUp = true;
            Destroy(farLeftLine);
            Destroy(lessLeftLine);
            Destroy(lessRightLine);
            Destroy(farRightLine);
            GetComponent<SpriteRenderer>().enabled = true;
            //lazerUpSFX.Play();
        }
    }

}
