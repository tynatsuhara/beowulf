﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			SceneManager.LoadScene("heorot1");
		}
	}
}
