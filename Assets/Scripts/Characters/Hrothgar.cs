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
		if (state == State.WAITING && c.PlayerWithinDistance(1.5f)) {
			state = State.TALKING;
			Conversation.instance.StartConversation(greetingBeowulf, c, GameManager.instance.player);
		} else if (state == State.TALKING && Conversation.instance.ConversationComplete) {
			c.Move(1f, 0f);
		}
	}

	private string[] greetingBeowulf = {
		"0 Hello stranger!",
		"0 Who are you?",
		"1 I am Beowulf!"
	};
}
