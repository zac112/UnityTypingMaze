using UnityEngine;

public class BlinkTriggerTile : Tile
{
	protected override void ApplyEffect(GameObject collider)
	{
		//collider.GetComponent<Player>().GivePlayerEffect(new SurroundingWallBlinkEffect());
		Maze.Instance().ActivateRandomFloors();
		Maze.Instance().ActivateBlinkingWalls();
		this.TransformTo<Tile>();
	}
}