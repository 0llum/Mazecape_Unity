using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float Distance;
	public float Speed;
	public float RotationSpeed;
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

	private void Update () {
		if (_moving) {
			return;
		}
		
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
				_startTime = Time.time;
				_startPosition = touch.position;
			} else if (touch.phase == TouchPhase.Ended) {
				_endTime = Time.time;
				_endPosition = touch.position;

				_swipeDistance = (_endPosition - _startPosition).magnitude;
				_swipeTime = _endTime - _startTime;

				if (_swipeTime > MaxSwipeTime) {
					return;
				}

				if (_swipeDistance < MinSwipeDistance) {
					return;
				}

				Vector2 distanceSwipe = _endPosition - _startPosition;

				if (Mathf.Abs(distanceSwipe.x) > Mathf.Abs(distanceSwipe.y)) {
					if (distanceSwipe.x > 0) {
						SwipeRight();
					} else if (distanceSwipe.x < 0) {
						SwipeLeft();
					}
				} else if (Mathf.Abs(distanceSwipe.x) < Mathf.Abs(distanceSwipe.y)) {
					if (distanceSwipe.y > 0) {
						SwipeUp();
					} else if (distanceSwipe.y < 0) {
						SwipeDown();
					}
				}		
			}
		}

		if (Input.GetKey (KeyCode.W)) {
			SwipeDown();			
		} else if (Input.GetKey (KeyCode.S)) {
			SwipeUp();
		} else if (Input.GetKey (KeyCode.A)) {
			SwipeRight();		
		} else if (Input.GetKey (KeyCode.D)) {
			SwipeLeft();
		}
	}

	private void SwipeUp() {
		if (!Down) {
			return;
		}
		
		StartCoroutine(TurnInDirection(new Vector3(0, 0, -1)));
		StartCoroutine(MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z - Distance)));
	}
	
	private void SwipeDown() {
		if (!Up) {
			return;
		}
		
		StartCoroutine(TurnInDirection(new Vector3(0, 0, 1)));
		StartCoroutine(MoveToPosition (new Vector3 (transform.position.x, transform.position.y, transform.position.z + Distance)));
	}
	
	private void SwipeLeft() {
		if (!Right) {
			return;
		}
		
		StartCoroutine(TurnInDirection(new Vector3(1, 0, 0)));
		StartCoroutine(MoveToPosition (new Vector3 (transform.position.x + Distance, transform.position.y, transform.position.z)));
	}
	
	private void SwipeRight() {
		if (!Left) {
			return;
		}
		
		StartCoroutine(TurnInDirection(new Vector3(-1, 0, 0)));
		StartCoroutine(MoveToPosition (new Vector3 (transform.position.x - Distance, transform.position.y, transform.position.z)));
	}

	public IEnumerator MoveToPosition(Vector3 newPosition) {		
		_moving = true;
		GameManager.Instance.IncreaseSteps();
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

	public IEnumerator TurnInDirection(Vector3 newDirection) {
		Vector3 oldDirection = transform.forward;

		while (oldDirection != newDirection) {
			transform.forward = Vector3.RotateTowards(oldDirection, newDirection, Time.deltaTime * RotationSpeed, 0);
			oldDirection = transform.forward;
			yield return null;
		}
	}
}
