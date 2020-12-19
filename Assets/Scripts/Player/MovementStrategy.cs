using UnityEngine;

public abstract class MovementStrategy {

	public abstract bool ValidDestination(Vector2 destination);
	public virtual Vector2 GetDirection(string kc){ return Vector2.zero; }
}
