using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool buttonPressed;
	public AudioSource buttonPressAudio;

	public Sprite down, up;

	public void OnPointerDown(PointerEventData eventData) {
		buttonPressAudio.Play();
		buttonPressed = true;
		GetComponent<Image>().sprite = down;
	}

	public void OnPointerUp(PointerEventData eventData) {
		buttonPressed = false;
		GetComponent<Image>().sprite = up;
	}
}