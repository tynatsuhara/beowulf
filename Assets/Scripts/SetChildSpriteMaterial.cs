using UnityEngine;
using System.Linq;

public class SetChildSpriteMaterial : MonoBehaviour {
	
	public Material material;

	void Awake () {
		GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(x => x.material = material);
	}
}
