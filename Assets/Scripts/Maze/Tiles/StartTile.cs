using UnityEngine;

public class StartTile : Tile
{
	void Awake()
	{
		Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject)), transform.position, Quaternion.identity);
	}
}
