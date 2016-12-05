using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Weapon : MonoBehaviour {

	public float damage;
	public float speed;
	public bool isAttacking;
	private float distLeft;
	public float attackDist;
	
	public void Attack() {
		if (isAttacking)
			return;

		isAttacking = true;
		distLeft = 360;

		List<GameObject> all = GameManager.instance.AllDamageableIntersectingPoint(transform.position 
				+ 1.3f * Vector3.right * transform.root.localScale.x 
				+ Vector3.up * .3f, transform.root.gameObject);
		all.AddRange(GameManager.instance.AllDamageableIntersectingPoint(transform.position 
				+ .4f * Vector3.right * transform.root.localScale.x
				+ Vector3.up * .3f, transform.root.gameObject));
		HashSet<GameObject> unique = new HashSet<GameObject>(all);
		unique.ToList().ForEach(x => x.GetComponent<Damageable>().Damage(damage, x.transform.position - transform.position));
	}

	public void Update() {
		if (isAttacking) {
			distLeft -= speed;
			transform.RotateAround(transform.position, transform.forward, -speed);
			if (distLeft <= 0) {
				isAttacking = false;
				transform.rotation = Quaternion.identity;
			}
		}
	}
}
