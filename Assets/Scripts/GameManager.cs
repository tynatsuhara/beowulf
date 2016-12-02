using UnityEngine;
using System.Linq;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Character player;
	public static GameManager instance;

	public GameObject grendelPrefab;

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
		StartCoroutine("GrendelFight");
	}
	private IEnumerator GrendelFight() {
		GameObject grendel = Instantiate(grendelPrefab) as GameObject;		
		CameraFollow.instance.TrackPosition(grendel.transform.position);
		yield return new WaitForSeconds(2f);
		CameraFollow.instance.TrackPlayer();
		yield return new WaitForSeconds(1f);
		player.GetComponent<Player>().GetNaked();
	}
}
