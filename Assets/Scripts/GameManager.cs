using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Character player;
	public static GameManager instance;

	void Start() {
		if (instance != null)
			Destroy(this);
		instance = this;
	}

	void Update() {
		if (!player.isAlive) {
			// TODO: end game
		}
	}
}
