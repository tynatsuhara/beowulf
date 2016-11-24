using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Character c;

	void Start() {
		c = GetComponent<Character>();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			c.Attack();
		}
		if (Input.GetMouseButton(1)) {
			c.Block();
		} else {
			c.Unblock();
		}
		c.Move(Input.GetAxis("Horizontal") / 100, Input.GetAxis("Vertical") / 100);
		if (Input.GetAxisRaw("Horizontal") != 0) {
			transform.localScale = new Vector3(Mathf.Sign(Input.GetAxisRaw("Horizontal")), 1f, 1f);
		}
	}
}
