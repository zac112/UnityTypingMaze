using UnityEngine;
using System.Collections;

public class BlinkingWall : Wall
{

	public static float maxBlinkRate = 7f;
	public static float speed = 1f;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(Blink());
	}

	IEnumerator Blink()
	{
		float blinkRate = 1f;

		yield return new WaitForSeconds(Random.value+.5f);

		while (true) {
			if (AllowBlink())
				gameObject.renderer.enabled = !gameObject.renderer.enabled;
			blinkRate = Random.Range(0f, maxBlinkRate) + speed;
			yield return new WaitForSeconds(blinkRate);
		}
	}

	private bool AllowBlink()
	{
		return !(Player.Instance().GetLocation().x == gameObject.transform.position.x &&
				Player.Instance().GetLocation().y == gameObject.transform.position.y);
	}
}
