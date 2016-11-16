using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public GameObject weapon;
	public GameObject shield;
	public float health;
	public bool isAlive {
		get { return health > 0; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
