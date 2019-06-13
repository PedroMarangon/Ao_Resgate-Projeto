using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MenuScript : MonoBehaviour {


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
		controls = (ControlType)PlayerPrefs.GetInt("ctrl");
		controlText.text = controls.ToString();
		GoToMain();
		for(int i=0;i<highscore.Length;i++){
			highscore[i].text = PlayerPrefs.GetInt("high").ToString();
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

}

public enum ControlType { Accelerometer,Buttons}