using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public bool isBlocking {
		get { return blocking; }
	}
	private bool blocking;
	public Vector3 shieldShift;

	public void Block () {
		if (!blocking) {
			Vector3 shift = shieldShift;
			shift.x *= transform.root.localScale.x;
			transform.Translate(shift);
		}

		blocking = true;
	}

	public void Unblock() {
		if (blocking) {
			Vector3 shift = shieldShift;
			shift.x *= transform.root.localScale.x;
			transform.Translate(-shift);
		}

		blocking = false;
	}
}
