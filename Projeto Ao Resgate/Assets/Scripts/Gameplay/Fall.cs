﻿//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

	public Falling falling;
	GameManager manager;

	// Use this for initialization
	void Awake () {
		manager = GameManager.instance;
		GetComponent<BoxCollider2D>().size = falling.size;
		GetComponent<BoxCollider2D>().offset = falling.offset;
		GetComponent<SpriteRenderer>().sprite = falling.sprite;
		GetComponent<SpriteRenderer>().color = falling.colorMultiplier;
	}

	void FixedUpdate() {
		transform.Rotate(0, 0, 10 * falling.rotateSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Respawn")) {
			MoveTrampoline trampoline = FindObjectOfType<MoveTrampoline>();
			
			trampoline.score = (falling.type == FallingType.Person) ?
				(trampoline.score-falling.Pontuation) : trampoline.score;

			manager.UpdateUI(trampoline.score);

			if(trampoline.score <= 0 ) {
				manager.DoTimesUp();
			}

			Destroy(gameObject);
		}
	}
}