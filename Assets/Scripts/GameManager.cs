using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Character player;
	public static GameManager instance;
	public Collider2D exit;
	public AudioClip hit;

	public GameObject grendelPrefab;

	void Start() {
		instance = this;
	}

	void Update() {
		if (!player.isAlive) {
			GameUI.instance.ShowDeathText();
			if (Input.GetKeyDown(KeyCode.Return))
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void LateUpdate() {
		SortCharacterLayers();		
	}

	private void SortCharacterLayers() {
		int i = 0;
		Object.FindObjectsOfType<Character>()
			.OrderBy(x => x.stablePosition.y)
			.Reverse().Select(x => x.GetComponentsInChildren<SpriteRenderer>().ToList())
			.ToList().ForEach(x => x.ForEach(y => y.sortingOrder = y.sortingOrder % 10 + 10 * i++));
	}

	public List<GameObject> AllDamageableIntersectingPoint(Vector3 point, GameObject exclude) {
		return Object.FindObjectsOfType<GameObject>()
			.Where(x => x != exclude
				&& x.GetComponent<Damageable>() != null 
				&& x.GetComponent<Collider2D>() != null
				&& x.GetComponent<Collider2D>().bounds.Contains(point))
			.ToList();
	}

	// this code is fucking dumb
	public void PlayHitSound() {
		AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
	}

	// SCENES
	public void StartGrendelFight() {
		ObjectiveManager.instance.CompleteCurrentObjective();
		StartCoroutine("GrendelFight");
	}
	private IEnumerator GrendelFight() {
		GameObject grendel = Instantiate(grendelPrefab, new Vector3(10f, -3.5f, 0f), Quaternion.identity) as GameObject;		
		CameraFollow.instance.TrackPosition(grendel.transform.position);
		yield return new WaitForSeconds(2f);
		CameraFollow.instance.TrackPlayer();
		yield return new WaitForSeconds(1f);
		player.GetComponent<Player>().GetNaked();
	}

	public void EndGrendelFight() {
		ObjectiveManager.instance.CompleteCurrentObjective();
	}

	public void EndGrendelLevel() {
		GameUI.instance.ShowMoneyText(5000, 100);
		ObjectiveManager.instance.CompleteCurrentObjective();		
		exit.gameObject.SetActive(true);
	}
}
