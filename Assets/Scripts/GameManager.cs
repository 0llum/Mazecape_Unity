using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public GameObject MenuOverlay;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}	
	}

	public void SwitchScene(string newScene) {
		if (!SceneManager.GetActiveScene().name.Equals(newScene)) {
			SceneManager.LoadScene(newScene);
		}
	}

	public void ReplayLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		MenuOverlay.SetActive(false);
	}
}
