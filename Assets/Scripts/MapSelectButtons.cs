using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectButtons : MonoBehaviour
{
    [SerializeField] private List<Vector2> mapPoints = new List<Vector2>();
    [SerializeField] int map = 1;
    bool mouseover = false;
    Vector3 scale = Vector3.zero;
    void Start()
    {
        scale = transform.localScale;
        // Initialize or set up any necessary components here
    }
    private void OnMouseOver()
    {
        transform.localScale = scale * 1.2f;
        mouseover = true;
    }
    private void OnMouseExit()
    {
        mouseover = false;
        transform.localScale = scale;
    }
    private void OnMouseDown()
    {
        List<Vector2> points = mapPoints;
        ChosenMap chosenMap = FindObjectOfType<ChosenMap>();
        if (chosenMap != null)
        {
            chosenMap.changeMap(points, map);
        }
        FindObjectOfType<CodeCaller>().PlayClick();
    }

    private void Update()
    {
        try
        {
            if (FindObjectOfType<ChosenMap>().map == map)
            {
                SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = new Color(0.654088f, 0.654088f, 0.654088f, 1); // Highlight color
                }
                GetComponent<SpriteRenderer>().color = new Color(0.654088f, 0.654088f, 0.654088f, 1); // Highlight color
            }
            else if (!mouseover)
            {
                SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].color = Color.white; // Highlight color
                }
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        catch
        {

        }
    }
}
