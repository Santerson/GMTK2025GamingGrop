using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallToChangePlayerColor : MonoBehaviour
{
    [SerializeField] GameObject referenceObj = null;
    [SerializeField] int type = 0;
    GameObject otherReferenceObj = null;

    private void Start()
    {
        otherReferenceObj = GameObject.Find("DemoPlayer");
        if (type == 0)
        {
            GetComponent<Slider>().value = FindObjectOfType<playerColor>().r;
        }
        if (type == 1)
        {
            GetComponent<Slider>().value = FindObjectOfType<playerColor>().g;
        }
        if (type == 2)
        {
            GetComponent<Slider>().value = FindObjectOfType<playerColor>().b;
        }
    }

    public void changeR(float r)
    {
        FindObjectOfType<playerColor>().r = (int)r;
        SpriteRenderer renderer = referenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(r / 256, renderer.color.g, renderer.color.b);
        renderer = otherReferenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(r / 256, renderer.color.g, renderer.color.b);
    }
    public void changeG(float r)
    {
        FindObjectOfType<playerColor>().g = (int)r;
        SpriteRenderer renderer = referenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, r/256, renderer.color.b);
        renderer = otherReferenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, r / 256, renderer.color.b);
    }
    public void changeB(float r)
    {
        FindObjectOfType<playerColor>().b = (int)r;
        SpriteRenderer renderer = referenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, r/256);
        renderer = otherReferenceObj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, r / 256);
    }
}
