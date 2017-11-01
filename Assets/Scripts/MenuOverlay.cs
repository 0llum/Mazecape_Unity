using UnityEngine;
using System.Collections;

public class MenuOverlay : MonoBehaviour {
    public static MenuOverlay Instance;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }	
    }
}
