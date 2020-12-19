using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
	public GameObject spawningParent;
	public List<GameObject> randomTexts;
	public float minWaiting = .5f;
	public float maxWaiting = 1f;
	public bool CanSpawn { set; get; }

	void Start()
	{
		CanSpawn = true;
		StartCoroutine(KeepSpawning());
	}

	private IEnumerator KeepSpawning()
	{
		while (true) {
			while (!CanSpawn) yield return null;
			float wait = Random.Range(minWaiting, maxWaiting);
			SpawnRandomText();
			yield return new WaitForSeconds(wait);
		}
	}

	public void SpawnRandomText()
	{
		var text = NGUITools.AddChild(spawningParent, randomTexts.GetRandomValue());
		EventManager.Raise(new OnTextSpawnEvent(text.GetComponent<RandomText>()));
	}
}
public class OnTextSpawnEvent : GameEvent
{
	public readonly RandomText spawnedText;
	public OnTextSpawnEvent(RandomText spawnedText)
	{
		this.spawnedText = spawnedText;
	}
}