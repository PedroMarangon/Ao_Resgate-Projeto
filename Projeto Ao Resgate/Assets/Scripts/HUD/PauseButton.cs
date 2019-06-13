//Maded by Pedro M Marangon
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseButton : MonoBehaviour {

	public GameObject pauseScreen;
	[SerializeField]private float offset;
	[SerializeField]private float duration;
	private float originalY;

	//public SceneAsset menuScene;

	void Start () {
		Time.timeScale = 1;
		originalY = pauseScreen.GetComponent<RectTransform>().localPosition.y;
		pauseScreen.SetActive(false);
	}
	
	public void Pause () {
		pauseScreen.SetActive(true);
		GetRectTransform(true);
	}

	private void GetRectTransform(bool active) {
		RectTransform rect = pauseScreen.GetComponent<RectTransform>();
		if (active) rect.DOMoveY(0 + offset, duration).OnComplete(() => SetScale(0));
		else {
			Debug.Log("false");
			rect.DOMoveY(-1280, duration).OnComplete(() => SetScale(1));
			Debug.Log("aaa");
		}
	}

	private void SetScale(int scale) {
		Time.timeScale = scale;
		if (scale == 1) pauseScreen.SetActive(false);
	}

	public void Resume() {
		GetRectTransform(false);
	}

	public void GoToMenu() {
		Debug.Log("Loaded Menu Scene");
		//SceneManager.LoadScene(menuScene.name);
	}
}
