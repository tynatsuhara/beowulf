using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public Text objectiveText;
	
	void Update () {
		objectiveText.text = ObjectiveManager.instance.CurrentObjectiveText();
	}
}
