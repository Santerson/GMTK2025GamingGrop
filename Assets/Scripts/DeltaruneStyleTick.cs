using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaruneStyleTick : MonoBehaviour
{
    [SerializeField] private AudioSource tickSfx;
    SpriteRenderer refRenderer;

    private void Start()
    {
        refRenderer = GetComponent<SpriteRenderer>();
        if (refRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }
    }

    private void FixedUpdate()
    {
        if (refRenderer.color.a >= 0)
        {
            refRenderer.color = new Color(refRenderer.color.r, refRenderer.color.g, refRenderer.color.b, refRenderer.color.a - 0.03f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyable") || collision.CompareTag("GIANTFUCKOFFLAZER"))
        {
            tickSfx.Play();
            refRenderer.color = new Color(refRenderer.color.r, refRenderer.color.g, refRenderer.color.b, 0.5f);
        }
    }
}
