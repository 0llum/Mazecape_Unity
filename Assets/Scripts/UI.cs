using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static UI Instance;
    public GameObject MenuOverlay;
    public Text StepCounter;
    public Text TimeCounter;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }	
    }
}
