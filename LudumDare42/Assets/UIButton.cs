using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour {

    public GameObject fadePanel;

    public void LoadLevel(string LevelName = "MainMenu")
    {
        fadePanel.GetComponent<Animation>().Play("FadeOut");
        SceneManager.LoadScene(LevelName);
    }
}
