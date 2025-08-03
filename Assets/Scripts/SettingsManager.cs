using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.8f;

    public void changeMusicVolume(float f)
    {
        musicVolume = f;
        dynamicMusicVol[] objs = FindObjectsOfType<dynamicMusicVol>();
        foreach (dynamicMusicVol obj in objs)
        {
            obj.updateVolume();
        }
    }

    public void changeSfxVolume(float f)
    {
        sfxVolume = f;
        dynamicSfxVol[] objs = FindObjectsOfType<dynamicSfxVol>();
        foreach (dynamicSfxVol obj in objs)
        {
            obj.updateVolume();
        }
    }
}
