using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screennukepowerup : MonoBehaviour
{
    public void activate()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (obj.CompareTag("GIANTFUCKOFFLAZER"))
                {
                    FindObjectOfType<ObstacleGenerator>().lazerUp = false;
                }
                Destroy(obj.transform.parent.gameObject);
            }

        }
        Destroy(transform.parent.gameObject);
    }
}
