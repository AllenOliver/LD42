using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

    bool paused;
    public GameObject PausePanel;

    private void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {

            if (!paused)
            {
                paused = true;

            
                if (PausePanel)
                {
                    PausePanel.GetComponent<Animation>().Play("Open");
                }

            //Time.timeScale = 0f;
        }
            else
            {

            
                if (PausePanel)
                {

                    PausePanel.GetComponent<Animation>().Play("Close");

                }
                paused = false;

            }
        
    }
}
