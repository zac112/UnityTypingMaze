using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour
{

	public float maxBlinkRate = 7f;
	public float speed = 1f;

	void Start()
	{
		StartCoroutine(StartBlinking());
	}

	IEnumerator StartBlinking()
	{
		yield return new WaitForSeconds(Random.value + .5f);

		while (true) {
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
			float blinkRate = Random.Range(0f, maxBlinkRate) + speed;
			yield return new WaitForSeconds(blinkRate);
		}
	}
}