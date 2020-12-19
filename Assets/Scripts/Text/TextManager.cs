using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextManager : MonoBehaviour
{
	public string textFileName;
	public string textFilesFolder = "TextFiles";

	private List<string> words;
	public List<RandomText> texts { private set; get; }
	void Awake()
	{
		texts = new List<RandomText>();
		words = new List<string>();
		var textFile = Resources.Load(textFilesFolder + "/" + textFileName) as TextAsset;
		if (!textFile)
			throw new Exception("Textfile " + textFileName + " was not found or wasn't able to be loaded from the Resources folder!");

		words = textFile.text.Split(',').ToList();
		for (int i = 0; i < words.Count; i++) {
			words[i] = words[i].Trim();
		}
	}
	void OnEnable()
	{
		EventManager.Subscribe<OnTextSpawnEvent>(OnTextSpawnHandler);
		EventManager.Subscribe<OnSuccessfullyTypedTextEvent>(OnSuccessfullyTypedTextHandler);
	}
	void OnDisable()
	{
		EventManager.Unsubscribe<OnTextSpawnEvent>(OnTextSpawnHandler);
		EventManager.Unsubscribe<OnSuccessfullyTypedTextEvent>(OnSuccessfullyTypedTextHandler);
	}
	private void OnTextSpawnHandler(OnTextSpawnEvent e)
	{
		var word = words.GetRandomValue();
		e.spawnedText.Text = word;
		texts.Add(e.spawnedText);
		words.Remove(word);
		if (words.Count == 0)
		{
			GameObject.Find("TextSpawner").GetComponent<TextSpawner>().CanSpawn = false;
			print("can't spawn anyMORE!!!");
		}
	}
	private void OnSuccessfullyTypedTextHandler(OnSuccessfullyTypedTextEvent e)
	{
		RemoveText(e.typedText);
		words.Add(e.typedText.Text);
		var spawner = GameObject.Find("TextSpawner").GetComponent<TextSpawner>();
		if (!spawner.CanSpawn)
			spawner.CanSpawn = true;
	}

	public void RemoveText(RandomText currentTypedText)
	{
		texts.Remove(currentTypedText);
		Destroy(currentTypedText.gameObject);
	}
}