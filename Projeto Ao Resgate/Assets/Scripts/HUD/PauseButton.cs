//Maded by Pedro M Marangon
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;
using UnityEngine.Audio;

public class PauseButton : MonoBehaviour {

	public GameObject pauseScreen;
	[SerializeField]private float offset;
	[SerializeField]private float settings_offset;
	[SerializeField]private float duration;
	private float originalY;

	[BoxGroup("Audio")] public AudioMixer mixer;
	[Space]
	[BoxGroup("Audio")] public GameObject sfxOn;
	[BoxGroup("Audio")] public GameObject sfxOff;
	[Space]
	[BoxGroup("Audio")] public GameObject mscOn;
	[BoxGroup("Audio")] public GameObject mscOff;

	[BoxGroup("Controls")] public ControlType controls;
	[BoxGroup("Controls")] public TMP_Text controlText;

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

			Time.timeScale = 1;
			//Debug.Log("false");
			rect.DOMoveY(-1280-duration, duration).OnComplete(() => SetScale(1));
			//Debug.Log("aaa");
		}
	}

	private void SetScale(int scale) {
		Time.timeScale = scale;
		if (scale == 1) pauseScreen.SetActive(false);
	}

	public void Resume() {
		GetRectTransform(false);
	}

	public void Settings() {
		//Time.timeScale = 1;
		
		RectTransform rect = pauseScreen.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>();
		rect.localPosition = new Vector3(rect.localPosition.x - 720, rect.localPosition.y, rect.localPosition.z);
	}

	public void Back() {
		//Time.timeScale = 1;
		RectTransform rect = pauseScreen.GetComponent<RectTransform>().GetChild(0).GetComponent<RectTransform>();
		rect.localPosition = new Vector3(rect.localPosition.x + 720, rect.localPosition.y, rect.localPosition.z);
		
	}

	public void GoToMenu() {
		Debug.Log("Loaded Menu Scene");
		SceneManager.LoadScene("Menu");
	}


	#region Settings
	public void SetMusicVolume(bool state) {
		mscOn.SetActive(state);
		mscOff.SetActive(!state);
		mixer.SetFloat("musicVolume", (state) ? 0 : -80);
	}

	public void SetSFXVolume(bool state) {
		sfxOn.SetActive(state);
		sfxOff.SetActive(!state);
		mixer.SetFloat("sfxVolume", (state) ? 0 : -80);
	}

	public void SetControl() {
		if (controls == ControlType.Buttons && !SystemInfo.supportsAccelerometer)
			return;

		controls = (controls == ControlType.Accelerometer) ? ControlType.Buttons : ControlType.Accelerometer;
		controlText.text = controls.ToString();

		FindObjectOfType<MoveTrampoline>().UI.SetActive(controls == ControlType.Buttons);

		PlayerPrefs.SetInt("ctrl", (int)controls);
	}
	#endregion

}
