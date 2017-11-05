using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {
	public float Duration;
	private Light _sun;
	private Light _torch;
	private float _initialIntensity;
	private float _initialRange;	

	private void Start() {
		_sun = GameObject.Find("Sun").GetComponent<Light>();
		_torch = GetComponent<Light>();
		_initialIntensity = _sun.intensity;
		_initialRange = _torch.range;
	}


	private void Update () {
		_sun.intensity -= _initialIntensity / Duration * Time.deltaTime;
		_torch.range -= _initialRange / Duration * Time.deltaTime;
	}
}
