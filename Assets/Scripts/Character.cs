using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour, Damageable {

	public Weapon weapon;
	public Shield shield;
	public float maxHealth;
	public float health;
	public float speed;
	public bool isAlive {
		get { return health > 0; }
	}
	public bool isBlocking {
		get { return shield != null && shield.isBlocking; }
	}
	public bool isWalking;
	private Vector3 animationOffset;
	public Vector3 stablePosition {
		get { return transform.position - animationOffset; }	
	}
	private Speech speech;

	void Start() {
		StartCoroutine("WalkAnimation");
		SpawnSpeech();
		health = maxHealth;
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
		Vector3 beforePos = transform.position;
		transform.Translate(currentSpeed * new Vector3(dx, dy, 0f));
		isWalking = dx != 0 || dy != 0;
	}

	public void Face(Vector3 point) {
		transform.localScale = new Vector3(point.x < transform.position.x ? -1 : 1, transform.localScale.y, 1);
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
		GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y).normalized * (!wasAlive ? 0f : (isAlive ? 5f : 10f)), ForceMode2D.Impulse);
		if (wasAlive && !isAlive) {
			transform.RotateAround(transform.position, Vector3.forward, 90 * (Random.Range(0, 2) == 0 ? -1 : 1));
		}
		return;
	}

	private IEnumerator WalkAnimation() {
		int dir = 1;  // 1 == going up, -1 == going down
		int leftRightDir = 1;
		while (true) {
			if (!isAlive)
				yield break;
			Vector3 initialPos = transform.position;			
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
			animationOffset += transform.position - initialPos;			
			yield return new WaitForSeconds(isBlocking && dir == 1 ? .2f : .13f);
		}
	}

	private void SpawnSpeech() {
		speech = (Instantiate(Resources.Load("speech")) as GameObject).GetComponent<Speech>();
		speech.character = transform;
	}
}
