using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float distance;
	public float speed;
	private bool moving = false;
	private float startTime;
	private float endTime;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float swipeDistance;
	private float swipeTime;
	public float maxSwipeTime;
	public float minSwipeDistance;
    public bool left;
    public bool right;
    public bool up;
    public bool down;

	void Update () {
		if (Input.touchCount > 0 && !moving) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				startTime = Time.time;
				startPosition = touch.position;
			} else if (touch.phase == TouchPhase.Ended) {
				endTime = Time.time;
				endPosition = touch.position;

				swipeDistance = (endPosition - startPosition).magnitude;
				swipeTime = endTime - startTime;

				if (swipeTime < maxSwipeTime && swipeDistance > minSwipeDistance) {
					Vector2 distanceSwipe = endPosition - startPosition;

					if (Mathf.Abs (distanceSwipe.x) > Mathf.Abs (distanceSwipe.y)) {
						if (distanceSwipe.x > 0 && left) {
							transform.forward = new Vector3 (-1, 0, 0);
							StartCoroutine (moveToPosition (new Vector3 (transform.position.x - distance, transform.position.y, transform.position.z)));
						} else if (distanceSwipe.x < 0 && right) {
							transform.forward = new Vector3 (1, 0, 0);
							StartCoroutine(moveToPosition (new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z)));
						}
					} else if (Mathf.Abs (distanceSwipe.x) < Mathf.Abs (distanceSwipe.y)) {
						if (distanceSwipe.y > 0 && down) {
							transform.forward = new Vector3 (0, 0, -1);
							StartCoroutine (moveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z - distance)));
						} else if (distanceSwipe.y < 0 && up) {
							transform.forward = new Vector3 (0, 0, 1);
							StartCoroutine(moveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z + distance)));
						}
					}
				}
			}
		}

		if (Input.GetKey (KeyCode.W) && up && !moving) {
			transform.forward = new Vector3 (0, 0, 1);
			StartCoroutine(moveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z + distance)));
		} else if (Input.GetKey (KeyCode.S) && down && !moving) {
			transform.forward = new Vector3 (0, 0, -1);
			StartCoroutine(moveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z - distance)));
		} else if (Input.GetKey (KeyCode.A) && left && !moving) {
			transform.forward = new Vector3 (-1, 0, 0);
			StartCoroutine(moveToPosition (new Vector3 (transform.position.x - distance, transform.position.y, transform.position.z)));
		} else if (Input.GetKey (KeyCode.D) && right && !moving) {
			transform.forward = new Vector3 (1, 0, 0);
			StartCoroutine(moveToPosition (new Vector3 (transform.position.x + distance, transform.position.y, transform.position.z)));
		}
	}

	IEnumerator moveToPosition (Vector3 newPosition) {
		moving = true;
		Vector3 oldPosition = transform.position;

		while (oldPosition != newPosition) {
			transform.position = Vector3.MoveTowards (oldPosition, newPosition, Time.deltaTime * speed);
			oldPosition = transform.position;
			yield return null;
		}

		moving = false;
	}
}
