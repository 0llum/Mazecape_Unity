using UnityEngine;

public class Goal : MonoBehaviour {
	public string NextLevel;
	private GameManager _gameManager;

	private void Start() {
		_gameManager = GameManager.Instance;
	}

	private void OnTriggerEnter(Collider other) {
		_gameManager.SwitchScene(NextLevel);
	}
}
