using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    float refScale = 0f;
    BoxCollider2D refCollider = null;
    float upTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        refScale = transform.lossyScale.x;
        transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
        refCollider = GetComponent<BoxCollider2D>();
        refCollider.enabled = false;
        upTime = FindObjectOfType<ObstacleGenerator>().lazerUptime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < refScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + refScale / 40, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.x > refScale)
            {
                transform.localScale = new Vector3(refScale, transform.localScale.y, transform.localScale.z);
                refCollider.enabled = false;
            }
        }
        else
        {
            upTime -= Time.deltaTime;
            if (upTime <= 0)
            {
                FindObjectOfType<ObstacleGenerator>().lazerUp = false;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
