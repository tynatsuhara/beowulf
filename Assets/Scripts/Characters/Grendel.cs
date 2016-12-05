using UnityEngine;
using System.Collections;

public class Grendel : MonoBehaviour {

	public Sprite[] sprites;
	private bool hasDied;
	private bool attacking;

	// Use this for initialization
	void Start () {
		StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
		Character c = GetComponent<Character>();
		if (!hasDied && !c.isAlive) {
			hasDied = true;
			GetComponentInChildren<SpriteRenderer>().sprite = sprites[3];
			ObjectiveManager.instance.CompleteCurrentObjective();
		} else if (c.isAlive) {
			if (!attacking) {
				c.Face(GameManager.instance.player.stablePosition);
				Vector3 dir = GameManager.instance.player.stablePosition - transform.position;				
				if (dir.magnitude < 2f) {
					c.Move(0, 0);					
					StartCoroutine("Attack");
				} else {
					c.Move(dir.x, dir.y);
				}
			}
		}
	}

	private IEnumerator Attack() {		
		attacking = true;
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
		yield return new WaitForSeconds(.3f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
		yield return new WaitForSeconds(.2f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
		yield return new WaitForSeconds(.8f);					
		attacking = false;	
	}
}
