using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Weapon weapon;
	public Shield shield;
	public float health;
	public float speed;
	public bool isAlive {
		get { return health > 0; }
	}
	public bool isBlocking {
		get { return shield != null && shield.isBlocking; }
	}

	public void Move(float dx, float dy) {
		float currentSpeed = isBlocking ? speed / 1.5f : speed;
		transform.Translate(currentSpeed * new Vector3(dx, dy, 0f));
	}

	public void Damage(float amount, GameObject attacker) {
		health -= amount;
	}

	public void Attack() {
		if (weapon != null)
			weapon.Attack();
	}

	public void Block() {
		if (shield != null)
			shield.Block();
	}

	public void Unblock() {
		if (shield != null)
			shield.Unblock();
	}
}
