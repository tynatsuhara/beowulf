﻿using UnityEngine;
using System.Collections;

public class Hrothgar : MonoBehaviour {

	private Character c;

	private enum State {
		WAITING,
		TALKING,
		WAITING_FOR_GRENDEL
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
			ObjectiveManager.instance.CompleteCurrentObjective();
			GameManager.instance.Invoke("StartGrendelFight", 5f);
			state = State.WAITING_FOR_GRENDEL;
		}
	}

	private string[] greetingBeowulf = {
		"0 Hello stranger!",
		"0 Who are you?",
		"1 I am Beowulf!",
		"0 Why are you here, Beowulf?",
		"1 I have come to save you",
		"1 from the monster Grendel!",
		"0 Many men have failed",
		"0 How do we know you will succeed?",
		"1 I killed some sea monsters",
		"0 Finally, a true hero",
		"0 Thank non-Christian God!"
	};
}
