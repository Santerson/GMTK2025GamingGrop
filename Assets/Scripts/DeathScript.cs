using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [SerializeField] float waitTime = 2;
    [SerializeField] GameObject window;
    public void Death()
    {
        StartCoroutine(showDeathWindow());
    }

    IEnumerator showDeathWindow()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<WindowOpener>().Open(window);
    }
}
