﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speech : MonoBehaviour {

	public Text text;
	public Transform character;
	
	void Update () {
		if (character == null)
			return;
		transform.position = character.GetComponent<Character>().stablePosition;
	}

	private string[] lines;
	public void Say(string[] lines) {
		this.lines = lines;
		StartCoroutine("SayLines");
	}

	private IEnumerator SayLines() {
		foreach (string s in lines) {
			text.text = s;
			yield return new WaitForSeconds(2f);
		}
		text.text = "";
	}
}
