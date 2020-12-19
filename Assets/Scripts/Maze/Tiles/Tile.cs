using UnityEngine;

public class Tile : MonoBehaviour
{
	protected virtual void OnTriggerEnter(Collider other)
	{
		ApplyEffect(other.gameObject);
	}

	protected virtual void ApplyEffect(GameObject collider) { }

	public T TransformTo<T>() where T : Tile
	{
		if (GetType() == typeof(T)) return (T)this;
		Destroy(this);
		return gameObject.AddComponent<T>();
	}
}