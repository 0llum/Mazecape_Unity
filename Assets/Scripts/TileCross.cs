using UnityEngine;

public class TileCross : MonoBehaviour {

	private PlayerMovement _playerMovement;

	private void Start () {
		_playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other) {
		_playerMovement.Up = true;
		_playerMovement.Down = true;
		_playerMovement.Left = true;
		_playerMovement.Right = true;
	}
}
