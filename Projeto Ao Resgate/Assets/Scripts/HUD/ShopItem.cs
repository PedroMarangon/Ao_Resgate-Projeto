//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {

	public PowerUp power = PowerUp.None;

	public MenuScript menu;

	// Use this for initialization
	void Start () {
		
	}

	public void ActivatePowerUp() {

		if (menu.HighScoreNeeded(power)) {
			StartCoroutine(menu.DoAd());

			if (menu.effect01 == PowerUp.None) menu.effect01 = power;
			else menu.effect02 = power;

			PlayerPrefs.SetInt("power01", (int)menu.effect01);
			PlayerPrefs.SetInt("power02", (int)menu.effect02);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
