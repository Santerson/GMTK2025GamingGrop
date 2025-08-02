using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicSfxVol : MonoBehaviour
{
    AudioSource source = null;
    float defaultVolume = 1;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        defaultVolume = source.volume;
        updateVolume();
    }
    // Update is called once per frame
    public void updateVolume()
    {
        source.volume = FindObjectOfType<SettingsManager>().sfxVolume * defaultVolume;

    }
}
