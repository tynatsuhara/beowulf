using UnityEngine;
using System.Collections;

public class Grendel : MonoBehaviour {

	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator Attack() {		
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
		yield return new WaitForSeconds(.1f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
		yield return new WaitForSeconds(.1f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];		
	}
}
