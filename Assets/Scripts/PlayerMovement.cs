using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float Distance;
	public float Speed;
	private bool _moving;
	private float _startTime;
	private float _endTime;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private float _swipeDistance;
	private float _swipeTime;
	public float MaxSwipeTime;
	public float MinSwipeDistance;
    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;
	public bool Teleported;
	private int _steps;

	private void Update () {
		if (Input.touchCount > 0 && !_moving) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				_startTime = Time.time;
				_startPosition = touch.position;
			} else if (touch.phase == TouchPhase.Ended) {
				_endTime = Time.time;
				_endPosition = touch.position;

				_swipeDistance = (_endPosition - _startPosition).magnitude;
				_swipeTime = _endTime - _startTime;

				if (_swipeTime < MaxSwipeTime && _swipeDistance > MinSwipeDistance) {
					Vector2 distanceSwipe = _endPosition - _startPosition;

					if (Mathf.Abs (distanceSwipe.x) > Mathf.Abs (distanceSwipe.y)) {
						if (distanceSwipe.x > 0 && Left) {
							transform.forward = new Vector3 (-1, 0, 0);
							StartCoroutine (MoveToPosition (new Vector3 (transform.position.x - Distance, transform.position.y, transform.position.z)));
						} else if (distanceSwipe.x < 0 && Right) {
							transform.forward = new Vector3 (1, 0, 0);
							StartCoroutine(MoveToPosition (new Vector3 (transform.position.x + Distance, transform.position.y, transform.position.z)));
						}
					} else if (Mathf.Abs (distanceSwipe.x) < Mathf.Abs (distanceSwipe.y)) {
						if (distanceSwipe.y > 0 && Down) {
							transform.forward = new Vector3 (0, 0, -1);
							StartCoroutine (MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z - Distance)));
						} else if (distanceSwipe.y < 0 && Up) {
							transform.forward = new Vector3 (0, 0, 1);
							StartCoroutine(MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z + Distance)));
						}
					}
				}
			}
		}

		if (Input.GetKey (KeyCode.W) && Up && !_moving) {
			transform.forward = new Vector3 (0, 0, 1);
			StartCoroutine(MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z + Distance)));
		} else if (Input.GetKey (KeyCode.S) && Down && !_moving) {
			transform.forward = new Vector3 (0, 0, -1);
			StartCoroutine(MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z - Distance)));
		} else if (Input.GetKey (KeyCode.A) && Left && !_moving) {
			transform.forward = new Vector3 (-1, 0, 0);
			StartCoroutine(MoveToPosition (new Vector3 (transform.position.x - Distance, transform.position.y, transform.position.z)));
		} else if (Input.GetKey (KeyCode.D) && Right && !_moving) {
			transform.forward = new Vector3 (1, 0, 0);
			StartCoroutine(MoveToPosition (new Vector3 (transform.position.x + Distance, transform.position.y, transform.position.z)));
		}
	}

	public IEnumerator MoveToPosition (Vector3 newPosition) {		
		_moving = true;
		_steps++;
		UI.Instance.StepCounter.text = _steps.ToString();
		Vector3 oldPosition = transform.position;

		while (oldPosition != newPosition) {
			transform.position = Vector3.MoveTowards (oldPosition, newPosition, Time.deltaTime * Speed);
			oldPosition = transform.position;
			yield return null;
		}

		_moving = false;

		if (!gameObject.activeSelf) {
			gameObject.SetActive(true);
		}
	}
}
