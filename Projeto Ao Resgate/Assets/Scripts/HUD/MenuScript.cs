using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System;

public class MenuScript : MonoBehaviour {


	/// <summary>
	/// The ad image is where actually, if i would launch the game on Play Store, it would contain an ad.
	/// However, for the purpose of this homework, I put an white image for 2 seconds :)
	/// </summary>
	[BoxGroup("Shop")] public Image ad;

	[BoxGroup("Shop")] public PowerUp effect01 = PowerUp.None, effect02 = PowerUp.None;
	[BoxGroup("Shop")] public Button[] buttons = new Button[4];


	[BoxGroup("HUD Movement")][InfoBox("Put the X/Y/Z values in the order that" +
											"the screens are in the Scene View")] public Vector3 xValues;
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
	[SerializeField] GameObject rel1, rel2, rel3, rel4;

	void Start () {
		Time.timeScale = 1;
		//disable the ad
		ad.enabled = false;
		//Get the saved things
		controls = (ControlType)PlayerPrefs.GetInt("ctrl");
		effect01 = (PowerUp)PlayerPrefs.GetInt("power01");
		effect02 = (PowerUp)PlayerPrefs.GetInt("power02");
		controlText.text = controls.ToString();
		//Define the main menu as the default
		GoToMain();
		//Set the high score value
		for(int i=0;i<highscore.Length;i++){
			highscore[i].text = PlayerPrefs.GetInt("high").ToString();
		}
		//Enable or disable the power-ups
		for (int i = 0; i < 4; i++) {
			int j = i + 1;
			if(!HighScoreNeeded((PowerUp)j)) {
				buttons[i].interactable = false;
				buttons[i].GetComponent<Outline>().enabled = false;
				buttons[i].GetComponentInChildren<Image>().color = buttons[i].colors.disabledColor;
			}else
				buttons[i].interactable = true;
		}

		if(VerifiesPlayerPrefs(1)) rel1.SetActive(false);
		if (VerifiesPlayerPrefs(2)) rel2.SetActive(false);
		if (VerifiesPlayerPrefs(3)) rel3.SetActive(false);
		if (VerifiesPlayerPrefs(4)) rel4.SetActive(false);
		
	}

	private bool VerifiesPlayerPrefs(int v) {
		return (PlayerPrefs.GetInt("power01") == v) || (PlayerPrefs.GetInt("power02") == v);
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
		SceneManager.LoadScene("Gameplay");
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
		if (controls == ControlType.Botões && !SystemInfo.supportsAccelerometer)
			return;

		controls = (controls == ControlType.Acelerômetro) ? ControlType.Botões : ControlType.Acelerômetro;
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
				t = (scr >= 1000);
				break;
			case PowerUp.x2:
				t = (scr >= 2000);
				break;
			case PowerUp.PeopleScoreDecrease:
				t = (scr >= 5000);
				break;
			case PowerUp.GrowTrampoline:
				t = (scr >= 10000);
				break;
		}

		return t;
	}

	public IEnumerator DoAd(GameObject reload) {
		ad.enabled = true;
		yield return new WaitForSeconds(2);
		ad.enabled = false;

		PlayerPrefs.SetInt("power01", (int)effect01);
		PlayerPrefs.SetInt("power02", (int)effect02);

		reload.SetActive(true);


	}
	#endregion

}

public enum ControlType { Acelerômetro,Botões}

public enum PowerUp {
	None,				//0
	GrowTrampoline,		//1
	x2,					//2
	TimeBoost,			//3
	PeopleScoreDecrease,//4
}