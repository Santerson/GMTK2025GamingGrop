using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psExplosiononDestroy : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;

    private void OnTriggerEnter()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
