using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public float Speed;
	private Vector3 _initPos;

	private void Start () {
		_initPos = transform.position;
	}
	
	private void Update () {
		
		transform.position = new Vector3(_initPos.x, _initPos.y - 0.5f + Mathf.Cos(2 * Mathf.PI * (Speed * 0.1f) * Time.time) / 2, _initPos.z);
	}

	private void OnTriggerEnter(Collider other) {
		GameManager.Instance.GameOver();
	}
}
