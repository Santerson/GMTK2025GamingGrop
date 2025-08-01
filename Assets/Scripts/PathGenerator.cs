using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{

    [SerializeField] public List<Vector2> PathNodes = new List<Vector2>();
    [SerializeField] private bool shouldBeCircle = true;
    [SerializeField] private Vector2 offset = Vector2.zero; // Offset to apply to the circle center
    [SerializeField] GameObject LinePrefab; // Prefab for the line object

    [SerializeField] GameObject map1bg;
    [SerializeField] GameObject map2bg;
    [SerializeField] GameObject map3bg;
    [SerializeField] GameObject map4bg;
    [SerializeField] GameObject map5bg;
    [SerializeField] GameObject map6bg;

    int map = 1;

    private void Awake()
    {
        try
        {
            PathNodes = FindObjectOfType<ChosenMap>().points; // Get the points from the ChosenMap script
            if (PathNodes == null || PathNodes.Count == 0)
            {
                shouldBeCircle = true; // If no points are set, default to drawing a circle
            }
            map = FindObjectOfType<ChosenMap>().map;
        }
        catch
        {
            shouldBeCircle = true;
            Debug.LogWarning("Could not find a map. defaulting to a circle. If you are starting the game from this scene, this is normal.");
        }
        if (shouldBeCircle)
        {
            PathNodes = DebugExtensions.DrawCircle(Vector2.zero, 3, Color.white, 64);
            for (int i = 0; i < PathNodes.Count; i++)
            {
                // Apply offset to each node
                PathNodes[i] += offset;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (map == 1)
            {
                map1bg.SetActive(true);
            }
            else if (map == 2)
            {
                map2bg.SetActive(true);
            }
            else if (map == 3)
            {
                map3bg.SetActive(true);
            }
            else if (map == 4)
            {
                map4bg.SetActive(true);
            }
            else if (map == 5)
            {
                map5bg.SetActive(true);
            }
            else if (map == 6)
            {
                map6bg.SetActive(true);
            }
        }
        catch
        {
            Debug.LogWarning("No map found. if this is on the main menu, this is normal.");
        }
        //Instantiate a line object between every point
        for (int i = 0; i < PathNodes.Count - 1; i++)
        {
            Vector2 start = PathNodes[i];
            Vector2 end = PathNodes[i + 1];
            // Create a new line object
            GameObject lineObject = Instantiate(LinePrefab);
            LineRenderer lineRenderer = lineObject.GetComponentInChildren<LineRenderer>();
            // Set the positions of the line renderer
            lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0));
            lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0));
        }
        //Instantiate a line between the last and first node
        GameObject refObj = Instantiate(LinePrefab);
        LineRenderer refLineRenderer = refObj.GetComponentInChildren<LineRenderer>();
        if (PathNodes.Count > 0)
        {
            Vector2 start = PathNodes[PathNodes.Count - 1];
            Vector2 end = PathNodes[0];
            refLineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0));
            refLineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        DebugExtensions.DrawCircle(Vector2.zero, 3, Color.white, 64);
        Gizmos.color = Color.red;
        for (int i = 0; i < PathNodes.Count - 1; i++)
        {
            Gizmos.DrawLine(PathNodes[i], PathNodes[i + 1]);
        }
        // Draw a sphere at each node
        foreach (var node in PathNodes)
        {
            Gizmos.DrawSphere(node, 0.1f);
        }
        // Draw a line between the last and first node
        if (PathNodes.Count > 0)
        {
            Gizmos.DrawLine(PathNodes[PathNodes.Count - 1], PathNodes[0]);
        }
    }
}
