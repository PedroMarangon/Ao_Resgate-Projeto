//Maded by Pedro M Marangon
using UnityEngine;

public class ShopItem : MonoBehaviour {

	public PowerUp power = PowerUp.None;

	public MenuScript menu;
	public GameObject reload;
	
	public void ActivatePowerUp() {

		if (menu.HighScoreNeeded(power) && Limit()) {

			if (menu.effect01 == PowerUp.None) menu.effect01 = power;
			else menu.effect02 = power;

			StartCoroutine(menu.DoAd(reload));

		}

	}

	bool Limit() {
		return (menu.effect01 != PowerUp.None && menu.effect02 == PowerUp.None) ||
				(menu.effect01 == PowerUp.None && menu.effect02 == PowerUp.None);
	}

}
