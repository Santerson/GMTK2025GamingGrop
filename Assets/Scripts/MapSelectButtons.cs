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
    }
    private void OnMouseDown()
    {
        List<Vector2> points = mapPoints;
        ChosenMap chosenMap = FindObjectOfType<ChosenMap>();
        if (chosenMap != null)
        {
            chosenMap.changeMap(points, map);
        }
    }

    private void Update()
    {
        try
        {
            if (FindObjectOfType<ChosenMap>().map == map && !mouseover)
            {
                transform.localScale = scale * 1.1f;
            }
            else if (!mouseover)
            {
                transform.localScale = scale;
            }
        }
        catch
        {

        }
    }
}
