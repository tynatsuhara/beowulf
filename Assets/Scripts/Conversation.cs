using UnityEngine;
using System.Collections.Generic;

public class Conversation : MonoBehaviour {

	public static Conversation instance;
	public float conversationSpeed;

	void Awake () {
		if (instance != null)
			Destroy(this);
		instance = this;
	}
	
	void Update () {
		PositionCamera();
	}

	private void PositionCamera() {
		if (characters == null || characters.Length == 0)
			return;
		Vector3 avgPos = new Vector3();
		foreach (Character c in characters)
			avgPos += c.transform.position;
		avgPos /= characters.Length;
		CameraFollow.instance.TrackPosition(avgPos);
	}

	private List<string> lines;
	public bool ConversationComplete {
		get { return lines.Count == 0; }
	}
	private Character[] characters;

	//   lines - each line in array should start with "X " where X is the Xth character param
	public void StartConversation(string[] lines, params Character[] characters) {
		this.lines = new List<string>(lines);
		this.characters = characters;
		this.lines.Insert(0, "");
		AdvanceLine();
	}

	public void AdvanceLine() {
		CancelInvoke("AdvanceLine");
		if (lines.Count == 0 || characters == null)
			return;
		foreach(Character c in characters)
			c.Say("");
		lines.RemoveAt(0);
		if (lines.Count == 0) {  // convo over
			characters = null;
			CameraFollow.instance.TrackPlayer();
			return;
		}
		Character speaker = characters[int.Parse(lines[0].Split(null)[0])];
		speaker.Say(lines[0].Substring(2));
		Invoke("AdvanceLine", conversationSpeed);
	}
}
