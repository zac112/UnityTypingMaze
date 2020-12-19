using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
	void Start()
	{
		SetPositionRandomly();
		Destroy(this);
	}
	void SetPositionRandomly()
	{
		UIWidget hiddenWidget = transform.parent.GetComponent<UIWidget>();
		Vector3 bottomLeft = hiddenWidget.localCorners[0];
		float h = hiddenWidget.height;
		float w = hiddenWidget.width;
		transform.localPosition = new Vector3(bottomLeft.x + w * Random.value, bottomLeft.y + h * Random.value, 0);
	}
}