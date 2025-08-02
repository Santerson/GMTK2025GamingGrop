using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsWindowMeteor : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Speed of the meteor
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(speed, ForceMode2D.Impulse);
    }
}
