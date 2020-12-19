using UnityEngine;

public class EndTile : Tile
{

	protected override void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Player>().CalculateScore();
		GameStateManager.AdvanceState();
	}
}