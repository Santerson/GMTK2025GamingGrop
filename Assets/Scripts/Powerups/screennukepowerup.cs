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
                Destroy(obj.transform.parent.gameObject);
            }
        }
        Destroy(transform.parent.gameObject);
    }
}
