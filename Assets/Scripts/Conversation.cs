using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {

	public static Conversation instance;
	public float conversationSpeed;

	void Awake () {
		if (instance != null)
			Destroy(this);
		instance = this;
	}
	
	void Update () {
	
	}

	private string[] lines;
	private Character[] characters;

	//   lines - each line in array should start with "X " where X is the Xth character param
	public void StartConversation(string[] lines, params Character[] characters) {
		this.lines = lines;
		this.characters = characters;
	}

	public void AdvanceLine() {
		CancelInvoke("AdvanceLine");
		if (lines.Length == 0)
			return;
		Character speaker = characters[int.Parse(lines[0].Split(null)[0])];
		speaker.Say(lines[0].Substring(2));
		Invoke("AdvanceLine", conversationSpeed);
	}
}
