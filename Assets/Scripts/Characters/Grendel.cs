using UnityEngine;
using System.Collections;

public class Grendel : MonoBehaviour {

	public Sprite[] sprites;
	private bool hasDied;
	private bool attacking;
	public Sprite arm;

	// Use this for initialization
	void Start () {
		StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
		Character c = GetComponent<Character>();
		if (!hasDied && !c.isAlive) {
			Die();
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
		if (!c.isAlive) {
			GetComponentInChildren<SpriteRenderer>().sprite = sprites[3];			
		}
	}

	private void Die() {
		hasDied = true;
		ObjectiveManager.instance.CompleteCurrentObjective();
		GameManager.instance.player.GetComponentInChildren<Shield>().GetComponent<SpriteRenderer>().sprite = arm;
	}

	private IEnumerator Attack() {		
		attacking = true;
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
		yield return new WaitForSeconds(.3f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
		GameManager.instance.AllDamageableIntersectingPoint(transform.position + Vector3.right * transform.root.localScale.x * 1.5f, transform.root.gameObject)
			.ForEach(x => x.GetComponent<Damageable>().Damage(1, transform.position - x.transform.position));
		yield return new WaitForSeconds(.2f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
		yield return new WaitForSeconds(.8f);					
		attacking = false;	
	}
}
