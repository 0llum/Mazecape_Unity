using UnityEngine;

public class TileT : MonoBehaviour {

	private PlayerMovement _playerMovement;
	private int _rotation;

	private void Start () {
		_playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
		_rotation = Mathf.RoundToInt(transform.rotation.eulerAngles.y);
	}

	private void OnTriggerEnter(Collider other) {
		switch (_rotation) {
			case 0:
				_playerMovement.Up = false;
				_playerMovement.Down = true;
				_playerMovement.Left = true;
				_playerMovement.Right = true;
				break;
			case 90:
				_playerMovement.Up = true;
				_playerMovement.Down = true;
				_playerMovement.Left = true;
				_playerMovement.Right = false;
				break;
			case 180:
				_playerMovement.Up = true;
				_playerMovement.Down = false;
				_playerMovement.Left = true;
				_playerMovement.Right = true;
				break;
			case 270:
				_playerMovement.Up = true;
				_playerMovement.Down = true;
				_playerMovement.Left = false;
				_playerMovement.Right = true;
				break;
			default:
				_playerMovement.Up = false;
				_playerMovement.Down = false;
				_playerMovement.Left = false;
				_playerMovement.Right = false;
				break;
		}
	}
}
