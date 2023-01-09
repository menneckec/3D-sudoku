using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //check if game is already paused       
            if (paused)
            {
                //unpause the game
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                paused = false;
            }
            //else if game isn't paused, then pause it
            else 
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                paused = true;
            }
        }
    }
}