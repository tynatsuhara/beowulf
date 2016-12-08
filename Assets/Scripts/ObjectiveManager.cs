using UnityEngine;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {

	public static ObjectiveManager instance;
	private List<Objective> objectives;
	
	void Awake() {
		instance = this;
		objectives = new List<Objective>();

		CreateObjective("AdventureBegins", "Talk to Hrothgar");
		CreateObjective("FirstParty", "Drink and party to draw Grendel to Heorot");
		CreateObjective("GrendelFight", "Defeat Grendel");
		CreateObjective("DefeatedGrendel", "Return to Hrothgar");
		CreateObjective("LeaveHeorot", "Depart from Heorot");
	}

	public void CompleteCurrentObjective() {
		if (objectives.Count > 0)
			objectives.RemoveAt(0);
	}

	public string CurrentObjectiveText() {
		return objectives.Count == 0 ? "" : objectives[0].text;
	}

	public string CurrentObjective() {
		return objectives.Count == 0 ? "" : objectives[0].key;
	}

	private void CreateObjective(string key, string text) {
		objectives.Add(new Objective(key, text));
	}

	private class Objective {
		public string key;
		public string text;
		public Objective(string key, string text) {
			this.key = key;
			this.text = text;
		}
	}
}
