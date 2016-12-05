using UnityEngine;
using System.Collections;

public class MaterialStore : MonoBehaviour {

	public static MaterialStore singleton;

	public Material whiteFlashMaterial;

	void Awake () {
		singleton = this;
	}
}
