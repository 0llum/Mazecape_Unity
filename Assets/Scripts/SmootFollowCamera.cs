using UnityEngine;
using System.Collections;

public class SmootFollowCamera : MonoBehaviour {

	public Transform lookAt;
	public Vector3 offset;
	public bool smooth;
	public float speed;

	void LateUpdate () {
		Vector3 desiredPosition = lookAt.transform.position + offset;

		if (smooth) {
			transform.position = Vector3.Lerp (transform.position, desiredPosition, speed);
		} else {
			transform.position = desiredPosition;
		}
	}
}
