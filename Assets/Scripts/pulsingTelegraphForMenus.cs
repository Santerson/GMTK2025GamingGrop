using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsingTelegraphForMenus : MonoBehaviour
{
    SpriteRenderer refRenderer = null;
    float baseAlpha = 1;

    bool called = false;
    // Start is called before the first frame update
    void Start()
    {
        refRenderer = GetComponent<SpriteRenderer>();
        baseAlpha = refRenderer.color.a;
    }

    private void FixedUpdate()
    {
        refRenderer.color = new Color(refRenderer.color.r, refRenderer.color.g, refRenderer.color.b, refRenderer.color.a - 0.003f);
        if (refRenderer.color.a <= 0 && !called)
        {
            StartCoroutine(waitUntilRespawn());
            called = true;
        }
    }

    IEnumerator waitUntilRespawn()
    {
        yield return new WaitForSeconds(3);
        refRenderer.color = new Color(refRenderer.color.r, refRenderer.color.g, refRenderer.color.b, baseAlpha);
        called = false;
    }
}
