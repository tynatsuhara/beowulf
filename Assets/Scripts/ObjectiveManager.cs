using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour {

	public ObjectiveManager instance;
	private List<Objective> objectives;
	
	void Awake() {
		if (instance != null)
			Destroy(this);
		instance = this;
		objectives = new List<Objective>();

		CreateObjective("AdventureBegins", "Visit Hrothgar in Denmark");
		CreateObjective("FirstParty", "Drink and party to draw Grendel to Heorot");
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
