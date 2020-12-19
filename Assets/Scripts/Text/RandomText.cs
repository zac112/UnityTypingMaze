using UnityEngine;

public class RandomText : MonoBehaviour
{
	[SerializeField]
	private Color charHighlight;
	[SerializeField]
	private Vector2 playerMoveDir;
	private UILabel mLabel;
	private string mText;
	private UILabel label { get { if (!mLabel) mLabel = GetComponent<UILabel>(); return mLabel; } }

	public Vector2 PlayerMoveDir { get { return playerMoveDir; } }

	public string Text { set { mText = value; label.text = value; } get { return mText; } }
	public void HighlightText()
	{
		label.effectStyle = UILabel.Effect.Outline;
	}
	public void ResetHighlight()
	{
		label.effectStyle = UILabel.Effect.None;
		label.text = mText;
	}
	public void HighlightChar(int atIndex)
	{
		label.text = mText;
		string charHighlightStr = ColorToHex(charHighlight);
		label.text = Text.Insert(Mathf.Min(atIndex + 1, mText.Length), "[-]");
		label.text = label.text.Insert(atIndex, "[" + charHighlightStr + "]");
	}
	string ColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}
	void OnCollisionEnter(Collision other)
	{
		print("Collision: " + this + " " + other);
	}
	void OnTriggerEnter(Collider other)
	{
		print("Trigger: " + this + " " + other);
	}
	public void Move(bool move)
	{
		GetComponent<TextMovement>().enabled = move;
	}
}