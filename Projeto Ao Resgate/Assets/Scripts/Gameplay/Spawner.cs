//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Spawner : MonoBehaviour {

	[ReorderableList] public List<Falling> whatFalls;

	public GameObject fallingThing;

	[MinMaxSlider(.1f,1f)]public Vector2 rangeForSpawn;

	public float timeToFall = 1;
	[ReadOnly]public float originalTime;

	void Awake() {
		originalTime = timeToFall;
		timeToFall *= 10;
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			if (timeToFall <= 0) {
				timeToFall = originalTime * 10;
				Falling fall = whatFalls[Random.Range(0, whatFalls.Count)];

				fallingThing.GetComponent<Fall>().falling = fall;

				GameObject GO = Instantiate(fallingThing, transform.position, transform.rotation);
				GO.GetComponent<BoxCollider2D>();
			} else {
				//timeToFall -= Random.Range(rangeForSpawn.x,rangeForSpawn.y);
				timeToFall -= .1f;
			}
		}
	}
}
