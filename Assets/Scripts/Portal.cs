using UnityEngine;

public class Portal : MonoBehaviour {
	public Portal Target;
	private PlayerMovement _playerMovement;

	private void Awake() {
		_playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other) {
		if (_playerMovement.Teleported) {
			_playerMovement.Teleported = false;			
			return;
		}

		_playerMovement.Teleported = true;
		_playerMovement.gameObject.SetActive(false);
		
		StartCoroutine(_playerMovement.MoveToPosition(Target.transform.position));
		
		_playerMovement.Up = false;
		_playerMovement.Down = false;
		_playerMovement.Left = false;
		_playerMovement.Right = false;		
	}
}
