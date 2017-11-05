using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	private bool _paused;
	private GameObject _menuOverlay;
	private float _startTime;
	private float _time;
	private float _steps;
	private int _stars;
	private bool _firstStart;

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

	private void Update() {
		if (_paused) {
			return;
		}

		_time = Time.time - _startTime;
		UI.Instance.TimeCounter.text = _time.ToString("f2");
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
		ResetSteps();
		ResetTimer();
		ResetStars();
	}

	public void ResetTimer() {
		_firstStart = true;
		_paused = true;
		UI.Instance.TimeCounter.text = 0f.ToString("f2");
	}

	public void PauseTimer() {
		_paused = true;
	}

	public void StartTimer() {
		_paused = false;
	}

	public void ResetSteps() {
		_steps = 0;
		UI.Instance.StepCounter.text = _steps.ToString("");
	}

	public void IncreaseSteps() {
		_steps++;
		UI.Instance.StepCounter.text = _steps.ToString("");

		if (_firstStart) {
			_firstStart = false;
			_startTime = Time.time;
			_paused = false;
		}
	}

	public void ResetStars() {
		_stars = 0;
	}

	public void IncreaseStars() {
		_stars++;
	}

	public void GameOver() {
		PauseTimer();
		UI.Instance.MenuOverlay.SetActive(true);
		PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
		playerMovement.Up = false;
		playerMovement.Down = false;
		playerMovement.Left = false;
		playerMovement.Right = false;
	}
}
