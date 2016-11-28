using UnityEngine;
using System.Collections;

public class Hrothgar : MonoBehaviour {

	private Character c;

	private enum State {
		WAITING,
		TALKING
	}
	private State state = State.WAITING;

	void Start () {
		c = GetComponent<Character>();
	}
	
	void Update () {
		if (state == State.WAITING && c.PlayerWithinDistance(1f)) {
			state = State.TALKING;
			Debug.Log("talk!");
			Conversation.instance.StartConversation(greetingBeowulf, c, GameManager.instance.player);
		}
	}

	private string[] greetingBeowulf = {
		"0 Hello stranger!",
		"0 Who are you?",
		"1 I am Beowulf!"
	};
}
