using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable"))
            Destroy(collision.transform.parent.gameObject);
    }
}
