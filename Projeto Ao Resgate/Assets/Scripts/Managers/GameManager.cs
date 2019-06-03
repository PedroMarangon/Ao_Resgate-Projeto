﻿//Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region Singleton
	public static GameManager instance;
	void Awake() {
		if(instance != null) {
			Destroy(gameObject);
		}
		instance = this;
	}
	#endregion

	public GameObject GameOver;
	[BoxGroup("Score")] public TMP_Text shadowText;
	[BoxGroup("Score")] public TMP_Text scoreText;
	
	public void UpdateUI(int score) {
		scoreText.text = score.ToString();
		shadowText.text = score.ToString();
	}

	public void DoGameOver() {
		Time.timeScale = 0;
		GameOver.SetActive(true);
	}

}
