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

    public void PlayClick()
    {
        try
        {
            GameObject.Find("Clicksfx").GetComponent<AudioSource>().Play();
        }
        catch
        {
            Debug.LogError("Click SFX not found!");
        }
    }

    public void ChangeMusicVol(float f)
    {
        FindObjectOfType<SettingsManager>().changeMusicVolume(f);
    }

    public void ChangeSfxVol(float f)
    {
        FindObjectOfType<SettingsManager>().changeSfxVolume(f);
    }

    public void stopMenuEFX()
    {
        FindObjectOfType<PSTrackerForMainMenu>().stop();
    }
}
