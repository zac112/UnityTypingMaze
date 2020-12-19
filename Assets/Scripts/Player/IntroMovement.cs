using UnityEngine;

public class IntroMovement : MovementStrategy
{
	public override bool ValidDestination(Vector2 destination)
	{
		return true;
	}

	public override Vector2 GetDirection(string kc)
	{
		switch (kc) {
			case "a":
				return new Vector2(-1, 0);
			case "d":
				return new Vector2(1, 0);
			case "w":
				return new Vector2(0, 1);
			case "s":
				return new Vector2(0, -1);
			default:
				return Vector2.zero;
		}
	}
}
