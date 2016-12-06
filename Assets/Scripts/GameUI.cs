using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public static GameUI instance;

	public Text objectiveText;
	public Text healthText;
	public Text[] retryGame;

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
}
