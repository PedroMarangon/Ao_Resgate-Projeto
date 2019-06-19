//Maded by Pedro M Marangon
using NaughtyAttributes; // Only for Inspector organization
using System.Collections;
using TMPro;//A better Text component
using UnityEngine;

public class TimerManager : MonoBehaviour {

	[InfoBox("Total time must be in seconds!")]public int totalTime;
	WaitForSeconds delay = new WaitForSeconds(1);//the delay between changes (in other words, 1 second)
	//Texts to display time left
	public TMP_Text timerTxt;
	public TMP_Text settingsTimerTxt;
	public TMP_Text shadowTxt;
	public Sprite p1, p2, p3, p4;
	//Instance of the Game Manager
	GameManager GM;
	public AudioSource tick;

	void Start() {
		//Get the GameManager
		GM = GameManager.instance;
		switch (PlayerPrefs.GetInt("power01")) {
			case 0:
				GM.power1.enabled = false;
				GM.power2.enabled = false;
				break;
			case 1:
				GM.power1.sprite = p1;
				break;
			case 2:
				GM.power1.sprite = p2;
				break;
			case 3:
				GM.power1.sprite = p3;
				break;
			case 4:
				GM.power1.sprite = p4;
				break;
		}
		switch (PlayerPrefs.GetInt("power02")) {
			case 0:
				GM.power2.enabled = false;
				break;
			case 1:
				GM.power2.sprite = p1;
				break;
			case 2:
				GM.power2.sprite = p2;
				break;
			case 3:
				GM.power2.sprite = p3;
				break;
			case 4:
				GM.power2.sprite = p4;
				break;
		}

		//If the Time Boost power up is activated, increase the time in 1 minute
		if (PlayerPrefs.GetInt("power01") == 3 || PlayerPrefs.GetInt("power02") == 3) totalTime += 60;
		StartCoroutine(TimerCoroutine());
	}

	IEnumerator TimerCoroutine() {
		for (; totalTime >= 0; --totalTime) {
			//Text in the correct format to show in screen
				// totalTime/60 : minutes
				// totalTime%60<=9 : second with a value between 0 and 9
				// totalTime%60==0 : second that the value is a multiple of 10
			string text = ((int)totalTime / 60).ToString() + ":" + (totalTime % 60 <= 9 ? "0" : "") + 
										((totalTime % 60 == 0) ? "0" : (totalTime % 60).ToString());

			if (totalTime == 10) tick.Play();

			//Set the text
			timerTxt.text = text;
			settingsTimerTxt.text = text;
			shadowTxt.text = text;
			//wait 1 second
			yield return delay;
		}

		//After the for loop, do the time's up
		GM.DoTimesUp();
	}
}
