using UnityEngine;

public class PitfallTile : Tile
{

	void Start()
	{
		renderer.material.mainTexture = (Texture2D)Resources.Load("Textures/Cracked_Dirt", typeof(Texture2D));
	}

	protected override void ApplyEffect(GameObject collider)
	{
		gameObject.renderer.enabled = false;
		StartCoroutine(FallDownHole(collider));
	}

	private System.Collections.IEnumerator FallDownHole(GameObject faller){
		AudioSource audio = gameObject.AddComponent<AudioSource>();

		float targetTime = 2f;
		float currentTime = 0;

		Player p;
		if (!(p = faller.GetComponent<Player>()))
			yield break;
		audio.PlayOneShot((AudioClip)Resources.Load("Sounds/Rockslide",typeof(AudioClip)));

		while(currentTime <= targetTime){
			Player.Instance().transform.localScale = Vector3.Lerp(Vector3.one,Vector3.zero,currentTime/targetTime);
			yield return null;
			currentTime += Time.deltaTime;
		}

		p.Die();
	}
}