using UnityEngine;
using System.Collections;
using System.Linq;

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
		if (Input.GetKeyDown(KeyCode.Space)) {
			Conversation.instance.AdvanceLine();
		}
		c.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (Input.GetAxisRaw("Horizontal") != 0) {
			transform.localScale = new Vector3(Mathf.Sign(Input.GetAxisRaw("Horizontal")), 1f, 1f);
		}
	}

	public void GetNaked() {
		c.Say("It's go time!");
		StartCoroutine("GetNakedHelper");		
	}
	private IEnumerator GetNakedHelper() {
		yield return new WaitForSeconds(.5f);
		GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "Tunic").enabled = false;
		yield return new WaitForSeconds(.5f);
		GetComponentsInChildren<SpriteRenderer>().First(x => x.name == "Pants").enabled = false;		
	}
}
