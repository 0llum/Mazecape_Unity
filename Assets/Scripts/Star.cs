using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public float RotationSpeed;
	public float HoverSpeed;
	private Vector3 _initPos;

	private void Start() {
		_initPos = transform.position;
	}

	private void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
		transform.position = new Vector3(_initPos.x, _initPos.y + Mathf.Sin(2 * Mathf.PI * (HoverSpeed * 0.1f) * Time.time) / 10, _initPos.z);
	}

	private void OnTriggerEnter(Collider other) {
		GameManager.Instance.IncreaseStars();
		Destroy(gameObject);
	}
}
