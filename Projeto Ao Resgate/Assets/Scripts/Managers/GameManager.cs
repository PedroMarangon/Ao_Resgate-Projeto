//Maded by Pedro M Marangon
using NaughtyAttributes; // Only for Inspector organization
using TMPro; //A better Text component
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Singleton system for being called anywhere
	#region Singleton
	public static GameManager instance;
	void Awake() {
		if(instance != null) {
			Destroy(gameObject);
		}
		instance = this;

		//Get the high score
		if(PlayerPrefs.GetInt("high") != 0){
			highscore = PlayerPrefs.GetInt("high");
		}
	}
	#endregion

	//Time's Up screen
	public GameObject TimesUp;

	public Image power1, power2;
	//current score and high score
	public int points,highscore;
	//Text that appears in screen to show actual score
	[BoxGroup("Score")] public TMP_Text shadowText;//shadow that have in text
	[BoxGroup("Score")] public TMP_Text scoreText;

	//score that appears on settings screen in play
	public TMP_Text scoreSettings;
	public AudioSource end;

	/// <summary>
	/// Update the UI to show the current score
	/// </summary>
	public void UpdateUI(int score) {
		//Set the texts
		scoreText.text = score.ToString();
		shadowText.text = score.ToString();
		scoreSettings.text = score.ToString();
		//Update the current score
		points = score;
	}

	/// <summary>
	/// Do the time's up
	/// </summary>
	public void DoTimesUp() {
		end.Play();
		//Pause the game
		Time.timeScale = 0;
		//Remove any power-up
		PlayerPrefs.SetInt("power01", 0);
		PlayerPrefs.SetInt("power02", 0);
		//Activate the time's up screen
		TimesUp.SetActive(true);
		//Set the highscore if the final round's score is greater than the old highscore
		if (points > highscore) {
			PlayerPrefs.SetInt("high",points);
			highscore=points;
		}
	}

	public void SceneLoads(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

}
