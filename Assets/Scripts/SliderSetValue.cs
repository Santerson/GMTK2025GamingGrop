using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetValue : MonoBehaviour
{
    [SerializeField] float version = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (version == 1)
        {
            GetComponent<Slider>().value = FindObjectOfType<SettingsManager>().musicVolume;
        }
        else
        {
            GetComponent<Slider>().value = FindObjectOfType<SettingsManager>().sfxVolume;
        }
    }
}
