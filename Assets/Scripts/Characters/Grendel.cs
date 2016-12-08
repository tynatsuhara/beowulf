using UnityEngine;
using System.Collections;
using System.Linq;

public class Grendel : MonoBehaviour {

	public Sprite[] sprites;
	private bool hasDied;
	private bool attacking;
	public Sprite arm;

	void Start () {
		GetComponent<Character>().Face(transform.position + Vector3.left);
		StartCoroutine("Attack");
	}
	
	void Update () {
		Character c = GetComponent<Character>();
		if (!hasDied && !c.isAlive) {
			Die();
		} else if (c.isAlive) {
			if (!attacking) {
				Character cp = ClosestPerson();
				c.Face(cp.stablePosition);
				Vector3 dir = cp.stablePosition - transform.position;
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

	private Character ClosestPerson() {
		return FindObjectsOfType<Character>()
				.Where(x => x.gameObject != gameObject && x.isAlive)
				.OrderBy(x => (x.transform.position - transform.position).magnitude)
				.First();
	}

	private void Die() {
		hasDied = true;
		GameManager.instance.EndGrendelFight();
		GameManager.instance.player.GetComponentInChildren<Shield>().GetComponent<SpriteRenderer>().sprite = arm;
	}

	private IEnumerator Attack() {
		attacking = true;
		yield return new WaitForSeconds(.2f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
		yield return new WaitForSeconds(.3f);
		if (!GetComponent<Character>().isAlive)
			yield break;
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
		FindObjectsOfType<GameObject>()
			.Where(x => x.GetComponent<Damageable>() != null &&
				   (x.transform.position - transform.position).magnitude < 2 &&
				   ((x.transform.position.x < transform.position.x && transform.root.localScale.x == -1) || 
				   (x.transform.position.x > transform.position.x && transform.root.localScale.x == 1)))
			.Distinct()
			.ToList()
			.ForEach(x => x.GetComponent<Damageable>().Damage(1, x.transform.position - transform.position));
		yield return new WaitForSeconds(.2f);
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
		yield return new WaitForSeconds(.8f);
		attacking = false;
	}
}
