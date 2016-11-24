using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float damage;
	public float speed;
	public bool isAttacking;
	public Transform rotationPoint;
	private float distLeft;
	
	public void Attack() {
		if (isAttacking)
			return;

		isAttacking = true;
		distLeft = 360;
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
