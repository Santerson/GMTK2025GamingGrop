using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    public bool gamePaused = false;
    // Update is called once per frame
    [SerializeField] GameObject deathwindow;
    [SerializeField] AudioSource bgm;
    GameObject refWindow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<Player>().dying && !gamePaused)
        {
            pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<Player>().dying && gamePaused)
        {
            unPause();
        }
    }

    void pause()
    {
        //TODO: Pause menu
        FindObjectOfType<ObstacleGenerator>().freeze();
        FindObjectOfType<Player>().halted = true;
        FindObjectOfType<Score>().stopCounting = true;
        gamePaused = true;
        refWindow = Instantiate(deathwindow, Vector2.zero, Quaternion.identity);
        bgm.Pause();
    }
    
    public void unPause()
    {
        //TODO: Unpause
        if (FindObjectOfType<Player>().started)
        {
            FindObjectOfType<ObstacleGenerator>().thaw();
            FindObjectOfType<Player>().halted = false;
            FindObjectOfType<Score>().stopCounting = false;
            bgm.Play();
        }
        gamePaused = false;
        Destroy(refWindow);
    }
}
