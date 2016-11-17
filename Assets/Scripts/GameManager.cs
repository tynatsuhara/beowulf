using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Character player;

	void Update() {
		if (!player.isAlive) {
			// TODO: end game
		}
	}
}
