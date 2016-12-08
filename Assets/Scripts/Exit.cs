using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponentInParent<Player>() != null) {
			GameUI.instance.FadeOut();
		}
	}
}
