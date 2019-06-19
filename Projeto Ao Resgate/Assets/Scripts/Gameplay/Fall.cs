//Maded by Pedro M Marangon
using UnityEngine;

public class Fall : MonoBehaviour {

	public Falling falling;//The thing that's fall
	GameManager manager;//Game Manager

	void Awake () {
		manager = GameManager.instance;//Get the manager
		
		//Set the size, offset, sprite and color of the object based on the Falling variable
		GetComponent<BoxCollider2D>().size = falling.size;
		GetComponent<BoxCollider2D>().offset = falling.offset;
		GetComponent<SpriteRenderer>().sprite = falling.sprite;
		GetComponent<SpriteRenderer>().color = falling.colorMultiplier;
	}

	void FixedUpdate() {
		//Rotate the object forever
		transform.Rotate(0, 0, 10 * falling.rotateSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Respawn")) {
			MoveTrampoline trampoline = FindObjectOfType<MoveTrampoline>();
			//boolean to remove points if the PeopleFall power-up isn't enabled
			bool tiraPontos = (PlayerPrefs.GetFloat("power01") != 4) || (PlayerPrefs.GetFloat("power02") != 4);
			//Remove points or not
			trampoline.score = (falling.type == FallingType.Person && tiraPontos) ?
				(trampoline.score-falling.Pontuation) : trampoline.score;
			//Clamp the minimum value of score in 0;
			if(trampoline.score <= 0) {
				trampoline.score = 0;
			}
			//Update the UI
			manager.UpdateUI(trampoline.score);
			Destroy(gameObject);
		}
	}
}
