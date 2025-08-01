using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpener : MonoBehaviour
{
    [SerializeField] private Vector2 position = Vector2.zero;

    private GameObject currentWindow;

    public void Open(GameObject window)
    {
        if (currentWindow != null)
        {
            Destroy(currentWindow);
        }
        currentWindow = Instantiate(window, position, Quaternion.identity);
    }
}
