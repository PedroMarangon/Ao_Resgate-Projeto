using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System;

public class MenuScript : MonoBehaviour {

	[BoxGroup("Shop")] public Image ad;
	[BoxGroup("Shop")] public PowerUp effect01 = PowerUp.None, effect02 = PowerUp.None;
	[BoxGroup("Shop")] public Button[] buttons = new Button[4];


	[BoxGroup("HUD Movement")][InfoBox("Put the X/Y/Z values in the order that the screens are in the Scene View")] public Vector3 xValues;
	[BoxGroup("HUD Movement")]public float MoveDuration = .5f;
	[BoxGroup("HUD Movement")]public RectTransform HUD;

	[BoxGroup("Audio")]public AudioMixer mixer;
	[Space]
	[BoxGroup("Audio")]public GameObject sfxOn;
	[BoxGroup("Audio")]public GameObject sfxOff;
	[Space]
	[BoxGroup("Audio")]public GameObject mscOn;
	[BoxGroup("Audio")]public GameObject mscOff;

	[BoxGroup("Controls")]public ControlType controls;
	[BoxGroup("Controls")] public TMP_Text controlText;

	[Space]
	[SerializeField]TMP_Text[] highscore;



	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		ad.enabled = false;
		controls = (ControlType)PlayerPrefs.GetInt("ctrl");
		effect01 = (PowerUp)PlayerPrefs.GetInt("power01");
		effect02 = (PowerUp)PlayerPrefs.GetInt("power02");
		controlText.text = controls.ToString();
		GoToMain();
		for(int i=0;i<highscore.Length;i++){
			highscore[i].text = PlayerPrefs.GetInt("high").ToString();
		}

		for (int i = 0; i < 4; i++) {
			int j = i + 1;
			if(!HighScoreNeeded((PowerUp)j)) {
				buttons[i].interactable = false;
				buttons[i].GetComponentInChildren<Image>().color = buttons[i].colors.disabledColor;
			}else
				buttons[i].interactable = true;
		}
		
	}

	#region Transitions
	public void GoToSettings(){
		HUD.DOMoveX(xValues.x,MoveDuration);
	}
	
	public void GoToMain(){
		HUD.DOMoveX(xValues.y,MoveDuration);
	}
		
	public void GoToShop(){
		HUD.DOMoveX(xValues.z,MoveDuration);
	}
	#endregion

	public void PlayGame(){
		SceneManager.LoadScene("test");
	}

	#region Settings
	public void SetMusicVolume(bool state){
		mscOn.SetActive(state);
		mscOff.SetActive(!state);
		mixer.SetFloat("musicVolume",(state)?0:-80);
	}

	public void SetSFXVolume(bool state){
		sfxOn.SetActive(state);
		sfxOff.SetActive(!state);
		mixer.SetFloat("sfxVolume",(state)?0:-80);
	}

	public void SetControl() {
		if (controls == ControlType.Buttons && !SystemInfo.supportsAccelerometer)
			return;

		controls = (controls == ControlType.Accelerometer) ? ControlType.Buttons : ControlType.Accelerometer;
		controlText.text = controls.ToString();
		PlayerPrefs.SetInt("ctrl", (int)controls);
	}
	#endregion

	#region Shop
	public bool HighScoreNeeded(PowerUp effect) {
		int scr = PlayerPrefs.GetInt("high");
		bool t=false;
		switch (effect) {
			case PowerUp.TimeBoost:
				t = (scr >= 500);
				break;
			case PowerUp.x2:
				t = (scr >= 1000);
				break;
			case PowerUp.PeopleScoreDecrease:
				t = (scr >= 1500);
				break;
			case PowerUp.GrowTrampoline:
				t = (scr >= 2000);
				break;
		}

		return t;
	}

	public IEnumerator DoAd() {
		ad.enabled = true;
		yield return new WaitForSeconds(2);
		ad.enabled = false;
	}
	#endregion

}

public enum ControlType { Accelerometer,Buttons}

public enum PowerUp {
	None,
	GrowTrampoline,		//1
	x2,					//2
	TimeBoost,			//3
	PeopleScoreDecrease,//4
}