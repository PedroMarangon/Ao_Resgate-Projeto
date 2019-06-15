//Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour {

	[InfoBox("Total time must be in seconds!")]public int totalTime;
	WaitForSeconds delay = new WaitForSeconds(1);
	public TMP_Text timerTxt;
	public TMP_Text settingsTimerTxt;
	public TMP_Text shadowTxt;

	GameManager GM;

	void Start() {
		GM = GameManager.instance;
		if (PlayerPrefs.GetInt("power01") == 3 || PlayerPrefs.GetInt("power02") == 3) totalTime += 60;
		StartCoroutine(TimerCoroutine());
	}

	IEnumerator TimerCoroutine() {
		for (; totalTime > 0; --totalTime) {
			string text = ((int)totalTime / 60).ToString() + ":" + (totalTime % 60 <= 9 ? "0" : "") + ((totalTime % 60 == 0) ? "0" : (totalTime % 60).ToString());
			timerTxt.text = text;
			settingsTimerTxt.text = text;
			shadowTxt.text = text;
			yield return delay;
		}

		//if (totalTime <= 0) {
			GM.DoTimesUp();
			//MovementScript.inputEnabled = false;
			//pointsTxt.text = "Você fez " + PointsScript.points.ToString() + " pontos!";
			//starsTxt.text = "Seu hotel recebeu " + (((PointsScript.points * 100) / 790) / 20).ToString() + " estrelas!";
			//WinScreen.SetActive(true);
	//	}

//		yield return new WaitForSeconds(5);

	//	SceneManager.LoadScene("Credits");
	}
}
