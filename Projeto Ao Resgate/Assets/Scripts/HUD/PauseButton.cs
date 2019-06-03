//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

	public GameObject pauseScreen;
	//public SceneAsset menuScene;

	void Start () {
		Resume();
	}
	
	public void Pause () {
		Time.timeScale = 0;
		pauseScreen.SetActive(true);
	}

	public void Resume() { 
		Time.timeScale = 1;
		pauseScreen.SetActive(false);
	}

	public void GoToMenu() {
		Debug.Log("Loaded Menu Scene");
		//SceneManager.LoadScene(menuScene.name);
	}
}
