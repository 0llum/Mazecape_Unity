using UnityEngine;

public class Goal : MonoBehaviour {
	public string NextLevel;
	private GameObject _menuOverlay;
	private PlayerMovement _playerMovement;

	private void Awake() {
		_playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}

	private void Start() {
		_menuOverlay = GameManager.Instance.MenuOverlay;
	}

	private void OnTriggerEnter(Collider other) {
		_menuOverlay.SetActive(true);
		
		_playerMovement.Up = false;
		_playerMovement.Down = false;
		_playerMovement.Left = false;
		_playerMovement.Right = false;		
	}
}
