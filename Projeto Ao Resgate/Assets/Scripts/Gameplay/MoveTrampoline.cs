﻿//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoveTrampoline : MonoBehaviour {

	GameManager manager;
	public GameObject UI;
	[BoxGroup("Global variables")] public float speed = 2f;
	[BoxGroup("Global variables")] public int score = 0;
	[BoxGroup("Global variables")] public float timeToDestroy = .5f;
	[BoxGroup("Accelerometer")][InfoBox("This is the variables that works with " +
		"Accelerometer enabled")]public bool hasAccelerometer = false;
	[BoxGroup("Accelerometer")]public float minValueForMove;

	[BoxGroup("Buttons")] public ButtonPressed left;
	[BoxGroup("Buttons")] public ButtonPressed right;

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		PlayerPrefs.SetInt("ctrl", 1);
#endif
		manager = GameManager.instance;
		Time.timeScale = 1;
		UI.SetActive(PlayerPrefs.GetInt("ctrl") == 1);//If ctrl=1 (buttons), sets to true
		//UI.SetActive(!hasAccelerometer);//If ctrl=1 (buttons), sets to true
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("ctrl") == 0) {
			if (SystemInfo.supportsAccelerometer) {
				if (Input.acceleration.x > minValueForMove || Input.acceleration.x < -minValueForMove) {
				Vector2 move = Input.acceleration * speed * Time.deltaTime;
				move.y = 0;
				transform.Translate(move);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x,-.8f, .8f),transform.position.y,transform.position.z);
				}
			} else {
				Debug.Log("Don't supports Accelerometer");
			}
		}
		else {
			if (left.buttonPressed) {
				transform.Translate(-(speed * Time.deltaTime), 0, 0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x,-.8f, .8f),transform.position.y,transform.position.z);
			} else
			if (right.buttonPressed) {
				transform.Translate(speed * Time.deltaTime, 0, 0);
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, -.8f, .8f), transform.position.y, transform.position.z);
			}

		}
	}

	public void RestartScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MenuScene() {
		SceneManager.LoadScene("Menu");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Fall>() != null) {
			Falling fall = other.GetComponent<Fall>().falling;
			switch (fall.type) {
				case FallingType.Obj:
					if (score > 0) {
						score -= fall.Pontuation;
						manager.UpdateUI(score);
						if (score <= 0) {
							manager.DoTimesUp();
						}
					}else manager.DoTimesUp();
				break;
				case FallingType.Person:
					score += fall.Pontuation;
					manager.UpdateUI(score);
				break;
			}
			Destroy(other.gameObject, timeToDestroy);
		}
	}

}
