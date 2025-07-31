using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    SpriteRenderer refRenderer;

    private void Start()
    {
        refRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        refRenderer.color = new Color(refRenderer.color.r, refRenderer.color.g, refRenderer.color.b, refRenderer.color.a - 0.01f);
        if (refRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
