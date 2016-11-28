using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, Damageable {

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
	public bool isWalking;
	public Vector3 stablePosition;	
	private Speech speech;

	void Start() {
		StartCoroutine("WalkAnimation");
		SpawnSpeech();
		stablePosition = transform.position;
	}

	public void Move(float dx, float dy) {
		if (new Vector3(dx, dy, 0).magnitude > 1) {
			Vector3 normalized = new Vector3(dx, dy, 0).normalized;
			dx = normalized.x;
			dy = normalized.y;
		}
		dx *= Time.deltaTime;
		dy *= Time.deltaTime;

		float currentSpeed = isBlocking ? speed / 1.5f : speed;
		transform.Translate(currentSpeed * new Vector3(dx, dy, 0f));
		stablePosition += currentSpeed * new Vector3(dx, dy, 0f);
		isWalking = dx != 0 || dy != 0;
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

	public void Say(string[] lines) {
		speech.Say(lines);
	}

	public void Say(string line) {
		speech.Say(new string[]{ line });
	}

	public bool PlayerWithinDistance(float dist) {
		return (transform.position - GameManager.instance.player.transform.position).magnitude < dist;
	}

	public void Damage(float amount, Vector3 direction) {
		bool wasAlive = isAlive;
		bool facingHit = Mathf.Sign(transform.root.localScale.x) != Mathf.Sign(direction.x);
		if (isBlocking && facingHit)
			amount *= .25f;
		health -= amount;
		if (!isAlive && wasAlive) {
			// TODO: knockback?
		}
		return;
	}

	private IEnumerator WalkAnimation() {
		int dir = 1;  // 1 == going up, -1 == going down
		int leftRightDir = 1;
		while (true) {
			if (!isWalking || dir == -1) {
				transform.rotation = Quaternion.identity;
			} else if (dir == 1) {
				transform.RotateAround(transform.position, Vector3.forward, 2.5f * leftRightDir);
				leftRightDir *= -1;
			}
			if (isWalking || dir == -1) {
				transform.position += transform.up * .07f * dir;
				dir *= -1;
			}
			yield return new WaitForSeconds(isBlocking && dir == 1 ? .2f : .13f);
		}	
	}

	private void SpawnSpeech() {
		speech = (Instantiate(Resources.Load("speech")) as GameObject).GetComponent<Speech>();
		speech.character = transform;
	}
}
