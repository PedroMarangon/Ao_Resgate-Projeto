//Maded by Pedro M Marangon
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

		if(PlayerPrefs.GetInt("high") != 0){
			highscore = PlayerPrefs.GetInt("high");
		}
	}
	#endregion

	public GameObject TimesUp;
	public int points,highscore;
	[BoxGroup("Score")] public TMP_Text shadowText;
	[BoxGroup("Score")] public TMP_Text scoreText;
	
	public void UpdateUI(int score) {
		scoreText.text = score.ToString();
		shadowText.text = score.ToString();
		points = score;
	}

	public void DoTimesUp() {
		Time.timeScale = 0;
		TimesUp.SetActive(true);
		if(points>highscore)
			PlayerPrefs.SetInt("high",points);
			highscore=points;
	}

}
