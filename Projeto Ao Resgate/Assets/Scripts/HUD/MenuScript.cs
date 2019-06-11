using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MenuScript : MonoBehaviour {

	public AudioMixer mixer;

	[BoxGroup("HUD Movement")][InfoBox("Put the X/Y/Z values in the order that the screens are in the Scene View")] public Vector3 xValues;
	[BoxGroup("HUD Movement")]public float MoveDuration = .5f;
	[BoxGroup("HUD Movement")]public RectTransform HUD;

	[BoxGroup("SFX")]public GameObject sfxOn;
	[BoxGroup("SFX")]public GameObject sfxOff;

	[BoxGroup("MUSIC")]public GameObject mscOn;
	[BoxGroup("MUSIC")]public GameObject mscOff;

	[SerializeField]TMP_Text[] highscore;



	// Use this for initialization
	void Start () {
		GoToMain();
		for(int i=0;i<highscore.Length;i++){
			highscore[i].text = PlayerPrefs.GetInt("high").ToString();
		}
		
	}

	public void GoToSettings(){
		HUD.DOMoveX(xValues.x,MoveDuration);
	}
	
	public void GoToMain(){
		HUD.DOMoveX(xValues.y,MoveDuration);
	}
		
	public void GoToShop(){
		HUD.DOMoveX(xValues.z,MoveDuration);
	}
	
	public void PlayGame(){
		SceneManager.LoadScene("test");
	}

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

}
