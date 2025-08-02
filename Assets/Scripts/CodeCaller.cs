using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeCaller : MonoBehaviour
{
    public void ContinueGame()
    {
        FindObjectOfType<pausemenu>().unPause();
    }

    public void KillPlayer()
    {
        FindObjectOfType<pausemenu>().unPause();
        FindObjectOfType<Player>().death();
        try
        {
            GameObject.Find("DamageSFX").GetComponent<AudioSource>().Play();
        }
        catch
        {

        }
    }
}
