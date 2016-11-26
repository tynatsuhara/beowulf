using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Character player;
	public float followSpeed;
	public float scrollMultiplier;
	
	void LateUpdate () {
		if (player == null)
			return;
		
		Vector3 toPos = player.stablePosition;
		toPos.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, toPos, followSpeed);

		Camera cam = GetComponent<Camera>();
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * -scrollMultiplier, .1f, 1.3f);
	}
}
