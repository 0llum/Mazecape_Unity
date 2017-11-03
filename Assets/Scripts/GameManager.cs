using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	private bool _paused;
	private GameObject _menuOverlay;
	private int _time;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}	
	}

	private void Start() {
		_menuOverlay = UI.Instance.MenuOverlay;
		ReplayLevel();
	}

	public void SwitchScene(string newScene) {
		if (!SceneManager.GetActiveScene().name.Equals(newScene)) {
			SceneManager.LoadScene(newScene);
		}
	}

	public void ReplayLevel() {
		StartLevel(SceneManager.GetActiveScene().name);
		_menuOverlay.SetActive(false);
	}

	public void NextLevel() {
		StartLevel(GameObject.Find("Goal").GetComponent<Goal>().NextLevel);
		_menuOverlay.SetActive(false);
	}

	private void StartLevel(string level) {
		SceneManager.LoadScene(level);
		ResetTimer();
		StartTimer();
	}

	public void ResetTimer() {
		_time = 0;
	}

	public void PauseTimer() {
		_paused = true;
	}

	public void StartTimer() {
		StartCoroutine(Timer());
	}
	
	private IEnumerator Timer() {
		while (!_paused) {
			yield return new WaitForSeconds(1);
			_time++;
			UI.Instance.TimeCounter.text = _time.ToString();
		}
	}
}
