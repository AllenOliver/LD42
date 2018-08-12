using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes names to load on button press")]
    [SerializeField] string OnPlayLoad;
    public void OnPlayPressed()
    {
        if (OnPlayLoad != "" && OnPlayLoad != null)
            SceneManager.LoadScene(OnPlayLoad);
        else
        {
            Debug.LogWarning("MainMenu.OnPlayPressed(): No scene name assigned to play button. Loading default scene with index 0");
            SceneManager.LoadScene(0);
        }
    }

    public void OnCreditsPressed()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
