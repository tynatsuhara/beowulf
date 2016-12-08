using UnityEngine;
using System.Collections;

public class Civilian : MonoBehaviour {

	private Character c;

	private Vector3 destination;

	void Awake() {
		c = GetComponent<Character>();
		destination = transform.position;
	}

	void Start () {
		StartCoroutine("MoveAbout");
	}

	void Update() {
		if (!c.isAlive)
			return;
		if (FindObjectOfType<Grendel>() != null) {
			destination = transform.position + (transform.position - FindObjectOfType<Grendel>().transform.position);
			if (!FindObjectOfType<Grendel>().GetComponent<Character>().isAlive) {
				destination = transform.position;
			}
		}
		if ((destination - transform.position).magnitude > .25f) {
			c.Move((destination - transform.position).x, (destination - transform.position).y);
		} else c.Move(0, 0);
	}
		
	private IEnumerator MoveAbout() {
		while (true) {
			if (!c.isAlive)
				yield break;
			yield return new WaitForSeconds(Random.Range(4f, 14f));
			destination = transform.position + (Vector3)Random.insideUnitCircle * Random.Range(.7f, 2f);
		}		
	}
}
