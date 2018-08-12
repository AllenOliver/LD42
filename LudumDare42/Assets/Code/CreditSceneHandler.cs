using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneHandler : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			SceneManager.LoadScene("MainMenu");
		}
	}

	public void LoadURL(string urlName) {
		Application.OpenURL(urlName);
	}
}
