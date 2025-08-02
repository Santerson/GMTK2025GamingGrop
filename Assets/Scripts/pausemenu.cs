using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    bool gamePaused = false;
    // Update is called once per frame
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
    }
    
    void unPause()
    {
        //TODO: Unpause
        FindObjectOfType<ObstacleGenerator>().thaw();
        FindObjectOfType<Player>().halted = false;
        FindObjectOfType<Score>().stopCounting = false;
        gamePaused = false;
    }
}
