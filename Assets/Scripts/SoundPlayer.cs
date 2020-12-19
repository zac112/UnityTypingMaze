using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
	public AudioClip[] sounds;

	IEnumerator OnTriggerEnter(Collider other)
	{
		DontDestroyOnLoad(this);
		audio.PlayOneShot(sounds[0]);
		yield return new WaitForSeconds(sounds[0].length / 2f);
		audio.PlayOneShot(sounds[1]);
		yield return new WaitForSeconds(sounds[1].length + 1f);
		Destroy(this);
	}
}