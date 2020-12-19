using UnityEngine;

public class LabyrinthMovement : MovementStrategy
{
	public override bool ValidDestination(Vector2 destination)
	{
		return Maze.Instance().AccessibleLocation(destination);
	}
}