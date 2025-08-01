using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpener : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefab; // Prefab for the window to open
    [SerializeField] private Vector2 position = Vector2.zero;

    private GameObject currentWindow;

    public void Open()
    {
        if (currentWindow != null)
        {
            Destroy(currentWindow);
        }
        currentWindow = Instantiate(windowPrefab, position, Quaternion.identity);
    }
}
