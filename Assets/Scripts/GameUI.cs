using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public static GameUI instance;

	public Text objectiveText;
	public Text healthText;
	public Text[] retryGame;
	public Text[] moneyText;
	public Image fadeColor;

	void Awake() {
		instance = this;
	}
	
	void Update () {
		objectiveText.text = ObjectiveManager.instance.CurrentObjectiveText();
		healthText.text = "HP: " + GameManager.instance.player.health + "/" + GameManager.instance.player.maxHealth;
	}

	public void ShowDeathText() {
		foreach (Text t in retryGame)
			t.gameObject.SetActive(true);
	}

	private int gold, goldLeft, xp, xpLeft;
	private float goldRate, xpRate;
	private float countTime = 2.5f;
	public void ShowMoneyText(int gold, int xp) {
		foreach (Text t in moneyText)
			t.gameObject.SetActive(true);
		moneyText[0].text = "GOLD: " + 0;
		moneyText[1].text = "XP: " + 0;
		this.gold = goldLeft = gold;
		this.xp = xpLeft = xp;
		goldRate = countTime/ 10 / gold;
		xpRate = countTime / xp;
		StartCoroutine("CountGold");
		StartCoroutine("CountXP");
	}
	private IEnumerator CountGold() {
		while (goldLeft > 0) {
			yield return new WaitForSeconds(goldRate);
			goldLeft -= 10;
			moneyText[0].text = "GOLD: " + (gold - goldLeft);
		}
		yield break;
	}
	private IEnumerator CountXP() {
		while (xpLeft > 0) {
			yield return new WaitForSeconds(xpRate);
			xpLeft--;
			moneyText[1].text = "XP: " + (xp - xpLeft);
		}
		yield break;
	}

	public void FadeOut() {
		StartCoroutine("FadeCoroutine");
	}
	private IEnumerator FadeCoroutine() {
		Color dark = fadeColor.color;
		dark.a = 1f;		
		while (fadeColor.color.a < .99f) {
			fadeColor.color = Color.Lerp(fadeColor.color, dark, .08f);
			yield return new WaitForSeconds(.01f);
		}
	}
}
