using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public static CameraFollow instance;

	public Character player;
	public float followSpeed;
	public float scrollMultiplier;

	private bool trackingPlayer = true;
	private Vector3 trackingPos;

	void Start() {
		if (instance != null)
			Destroy(this);
		instance = this;
	}
	
	void LateUpdate () {
		if (player == null)
			return;
		
		Vector3 toPos = trackingPlayer ? player.stablePosition : trackingPos;
		toPos.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, toPos, followSpeed);

		Camera cam = GetComponent<Camera>();
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * -scrollMultiplier, 1f, 10f);
	}

	public void TrackPlayer() {
		trackingPlayer = true;
	}

	public void TrackPosition(Vector3 pos) {
		trackingPlayer = false;
		trackingPos = pos;
	}
}
