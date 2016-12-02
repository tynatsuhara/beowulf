using UnityEngine;
using System.Linq;

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
		SortCharacterLayers();
	}

	private void SortCharacterLayers() {
		int i = 0;
		Object.FindObjectsOfType<Character>()
			.OrderBy(x => x.stablePosition.y)
			.Reverse().Select(x => x.GetComponentsInChildren<SpriteRenderer>().ToList())
			.ToList().ForEach(x => x.ForEach(y => y.sortingOrder = y.sortingOrder % 10 + 10 * i++));
	}


	// SCENES
	public void StartGrendelFight() {
		ObjectiveManager.instance.CompleteCurrentObjective();
	}
}
