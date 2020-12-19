using UnityEngine;

public class Wall : MonoBehaviour
{
	void Awake()
	{
		renderer.enabled = true;
	}

	public T TransformTo<T>() where T : Wall
	{
		if (this.GetType() == typeof(T)) return (T)this;

		Destroy(this);
		return gameObject.AddComponent<T>();
	}
}
