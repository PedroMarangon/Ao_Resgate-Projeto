using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool buttonPressed;

	public Sprite down, up;

	public void OnPointerDown(PointerEventData eventData) {
		buttonPressed = true;
		GetComponent<Image>().sprite = down;
	}

	public void OnPointerUp(PointerEventData eventData) {
		buttonPressed = false;
		GetComponent<Image>().sprite = up;
	}
}