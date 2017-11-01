using UnityEngine;

public class Goal : MonoBehaviour {
	private GameObject _menuOverlay;
	private PlayerMovement _playerMovement;

	private void Awake() {
		_menuOverlay = GameManager.Instance.MenuOverlay;
		_playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other) {
		_menuOverlay.SetActive(true);
		
		_playerMovement.Up = false;
		_playerMovement.Down = false;
		_playerMovement.Left = false;
		_playerMovement.Right = false;		
	}
}
