using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {

	float startRotation;
	float endRotation = 100f;
	public float currentTime = 0f;

	// Use this for initialization
	void Start () {
		startRotation = 70f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(startRotation,endRotation,currentTime/60f), 330f, 0f));
		currentTime += Time.deltaTime;
	}
}
