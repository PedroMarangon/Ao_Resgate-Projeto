//Maded by Pedro M Marangon
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Linq;

public class PauseButton : MonoBehaviour {

	public GameObject pauseScreen;

	[BoxGroup("Audio")] public AudioMixer mixer;
	[BoxGroup("Audio")] public AudioSource source;

	[Space]
	
	[BoxGroup("Audio")] public GameObject sfxOn;
	[BoxGroup("Audio")] public GameObject sfxOff;

	[Space]

	[BoxGroup("Audio")] public GameObject mscOn;
	[BoxGroup("Audio")] public GameObject mscOff;

	[BoxGroup("Controls")] public ControlType controls;
	[BoxGroup("Controls")] public TMP_Text controlText;


	List<AudioSource> sources;

	void Start () {
		Time.timeScale = 1;
		sources = new List<AudioSource>();
		pauseScreen.SetActive(false);
	}
	
	public void Pause () {
		source.Play();
		pauseScreen.SetActive(true);

		if (sources.Count > 0) sources.Clear();

		sources = FindObjectsOfType<AudioSource>().ToList();
		sources.Remove(source);
		foreach (AudioSource source in sources) {
			source.Pause();
		}
		Time.timeScale = 0;
	}
	public void Resume() {
		source.Play();
	
		if (sources.Count > 0) sources.Clear();

		sources = FindObjectsOfType<AudioSource>().ToList();
		sources.Remove(source);
		foreach (AudioSource source in sources) {
			source.UnPause();
		}
		Time.timeScale = 1;
	}

	public void GoToMenu() {
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
		if (controls == ControlType.Botões && !SystemInfo.supportsAccelerometer)
			return;

		controls = (controls == ControlType.Acelerômetro) ? ControlType.Botões : ControlType.Acelerômetro;
		controlText.text = controls.ToString();

		FindObjectOfType<MoveTrampoline>().UI.SetActive(controls == ControlType.Botões);

		PlayerPrefs.SetInt("ctrl", (int)controls);
	}
	#endregion

}
