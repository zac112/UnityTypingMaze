using System.Linq;
using UnityEngine;
using kc = UnityEngine.KeyCode;

public class TypingManager : MonoBehaviour
{
	public GameObject[] typingEffects;
	public AudioClip[] typingClips;
	public GameObject currentlyTypingArea;
	private TextManager textManager;
	private RandomText extraCurrentTypedText;
	private RandomText currentTypedText;
	private int typingIndex;
	private bool isTypingSomething;
	private string[] keys =
		{
			"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
			".", ",", "/", ">", "<"
		};
	public int Typos { private set; get; }
	public int TotalTypedLetters { private set; get; }

	void Awake()
	{
		textManager = GetComponent<TextManager>();
		typingIndex = 0;
		Typos = 0;
		TotalTypedLetters = 0;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Reset();

		for (int i = 0; i < keys.Length; i++) {
			var key = keys[i];
			if (Input.GetKeyDown(key)) {
				TotalTypedLetters++;
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
					key = char.ToUpper(key[0]).ToString();

				if (isTypingSomething) {
					ResumeTyping(key[0]);
				}
				else {
					var text = textManager.texts.FirstOrDefault(t => t.Text[0] == key[0]);
					if (text)
						StartTyping(text);
				}
			}
		}
	}

	private void ResumeTyping(char key)
	{
		string currentString = currentTypedText.Text;
		char currentChar = currentString[typingIndex];
		if (typingIndex < currentString.Length) {
			if (key == currentChar || char.ToLower(key) == currentChar)
			{
				print("correctly typed: " + key + " from: " + currentString);
				typingIndex++;
				currentTypedText.HighlightChar(typingIndex);
				extraCurrentTypedText.GetComponent<RandomText>().HighlightChar(typingIndex);
			}
			else Typos++;
		}
		if (typingIndex == currentString.Length) {
			// wrote a word correctly
			EventManager.Raise(new OnSuccessfullyTypedTextEvent(currentTypedText));
			var effect = Instantiate(typingEffects.GetRandomValue(), currentTypedText.transform.position, Quaternion.identity) as GameObject;
			effect.AddComponent<SelfDestroy>();
			AudioSource.PlayClipAtPoint(typingClips.GetRandomValue(), currentTypedText.transform.position);
			Reset();
		}
	}

	private void StartTyping(RandomText text)
	{
		text.HighlightText();
		currentTypedText = text;
		isTypingSomething = true;
		currentTypedText.Move(false);
		SetExtraTypingIndicator(text);
		ResumeTyping(currentTypedText.Text[0]);
	}

	private void SetExtraTypingIndicator(RandomText text)
	{
		extraCurrentTypedText = NGUITools.AddChild(currentlyTypingArea, text.gameObject).GetComponent<RandomText>();
		float wOffset = extraCurrentTypedText.GetComponent<UILabel>().width / 2 +
						extraCurrentTypedText.GetComponentInChildren<UISprite>().width / 2 +
						10;
		extraCurrentTypedText.transform.localPosition += new Vector3(wOffset, 0, 0);
		extraCurrentTypedText.Text = text.Text;
	}

	private void Reset()
	{
		if (currentTypedText) {
			currentTypedText.ResetHighlight();
			currentTypedText.Move(true);
			Destroy(extraCurrentTypedText.gameObject);
		}
		currentTypedText = null;
		isTypingSomething = false;
		typingIndex = 0;
	}
}

public class OnSuccessfullyTypedTextEvent : GameEvent
{
	public readonly RandomText typedText;
	public OnSuccessfullyTypedTextEvent(RandomText typedText)
	{
		this.typedText = typedText;
	}
}