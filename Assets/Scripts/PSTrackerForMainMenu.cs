using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTrackerForMainMenu : MonoBehaviour
{
    [SerializeField] ParticleSystem ps1;
    [SerializeField] ParticleSystem ps2;
    [SerializeField] ParticleSystem ps3;
    [SerializeField] ParticleSystem ps4;
    [SerializeField] ParticleSystem ps5;

    int active = 0;

    private void Start()
    {
        stop();
    }

    public void stop()
    {
        ps1.Stop();
        ps2.Stop();
        ps3.Stop();
        ps4.Stop();
        ps5.Stop();
    }

    public void changeActive(int f)
    {
        active = f;
        if (active == 1)
        {
            ps1.Play();
            ps2.Stop();
            ps3.Stop();
            ps4.Stop();
            ps5.Stop();
        }if (active == 2)
        {
            ps2.Play();
            ps1.Stop();
            ps3.Stop();
            ps4.Stop();
            ps5.Stop();
        }if (active == 3)
        {
            ps3.Play();
            ps2.Stop();
            ps1.Stop();
            ps4.Stop();
            ps5.Stop();
        }if (active == 4)
        {
            ps4.Play();
            ps2.Stop();
            ps3.Stop();
            ps1.Stop();
            ps5.Stop();
        }if (active == 5)
        {
            ps5.Play();
            ps2.Stop();
            ps3.Stop();
            ps4.Stop();
            ps1.Stop();
        }
    }

}
