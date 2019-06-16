//Maded by Pedro M Marangon
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;

public class Spawner : MonoBehaviour {

	//List of falling things
	[ReorderableList] public List<Falling> whatFalls;

	//GameObject with the Fall component
	public GameObject fallingThing;

	public AudioClip caindo;
	public AudioMixerGroup group;

	public float timeToFall = 1;//counter
	[ReadOnly]public float originalTime;//counterOriginalTime

	void Awake() {
		originalTime = timeToFall;
		timeToFall *= 10;
	}

	// Update is called once per frame
	void Update () {
		//First, check if the game isn't paused
		if (Time.timeScale != 0) {
			//if the counter is 0
			if (timeToFall <= 0) {
				timeToFall = originalTime * 10;
				Falling fall = whatFalls[Random.Range(0, whatFalls.Count)];

				fallingThing.GetComponent<Fall>().falling = fall;
				if (fallingThing.GetComponent<AudioSource>() == null) {
					fallingThing.AddComponent<AudioSource>();
				}

				fallingThing.GetComponent<AudioSource>().volume = 0.006f;
				fallingThing.GetComponent<AudioSource>().outputAudioMixerGroup = group;
				fallingThing.GetComponent<AudioSource>().loop = true;
				fallingThing.GetComponent<AudioSource>().clip = caindo;

				GameObject GO = Instantiate(fallingThing, transform.position, transform.rotation);
			} else {
				//Decrease time to fall
				timeToFall -= .1f;
			}
		}
	}
}
