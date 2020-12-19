using UnityEngine;

public class NullMovement : MovementStrategy
{
	public override bool ValidDestination(Vector2 destination)
	{
		return false;
	}
}
