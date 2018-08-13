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
    void Update() {
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
            //StartCoroutine(StopTime());

        }
        else
        {

            
            if (PausePanel)
            {
                //Time.timeScale = 1f;
                PausePanel.GetComponent<Animation>().Play("Close");

            }
            paused = false;

        }

    }

    IEnumerator StopTime()
    {

        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
}
