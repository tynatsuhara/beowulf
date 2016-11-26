using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speech : MonoBehaviour {

	public Text text;
	public Transform character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (character == null)
			return;
		transform.position = character.GetComponent<Character>().stablePosition;
	}
}
