using UnityEngine;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
	private static Maze mazeInstance;
	public static Maze Instance()
	{
		if (mazeInstance == null) {
			throw new UnityException("No Maze found in scene. Something has gone wrong!");
		}
		return mazeInstance;
	}

	[SerializeField]
	private GameObject floorPrefab;
	[SerializeField]
	private GameObject wallPrefab;

	[SerializeField]
	private int width;
	[SerializeField]
	private int height;
	[SerializeField]
	private GameObject[,] floors;
	[SerializeField]
	private GameObject[,] walls;

	[SerializeField]
	private int chanceToSpawnDamageTile;
	[SerializeField]
	private int chanceToSpawnPitfallTile;

	private EndTile endTile;

	void Awake()
	{
		floors = new GameObject[width, height];
		walls = new GameObject[width, height];
		mazeInstance = this;
	}

	void Start()
	{
		CreateEmptyMaze();
	}

	void CreateEmptyMaze()
	{
		for (int i=0; i < width; i++) {
			for (int j=0; j < height; j++) {
				floors[i, j] = (GameObject)Instantiate(floorPrefab, new Vector3(i, j, 0), Quaternion.identity);
				walls[i, j] = (GameObject)Instantiate(wallPrefab, floors[i, j].transform.position, Quaternion.identity);
			}
		}

		GameObject temp = (GameObject)Instantiate(floorPrefab, new Vector3(-1, Random.Range(0, width), 0), Quaternion.identity);
		temp.GetComponent<Tile>().TransformTo<StartTile>();
		temp = (GameObject)Instantiate(floorPrefab, new Vector3(width, Random.Range(0, height), 0), Quaternion.identity);
		endTile = temp.GetComponent<Tile>().TransformTo<EndTile>();

		CreateCuriousLightOnEndTile();

		PunchFirstPathThruMaze(Player.Instance().GetLocation(), temp.transform.position);
	}

	private void CreateCuriousLightOnEndTile(){
		GameObject go = Instantiate(Resources.Load("Prefabs/ParticleSystem",typeof(GameObject))) as GameObject;
		go.transform.position = endTile.transform.position;


	}

	private System.Collections.IEnumerator RandomizeFloorTiles(){
		float interval = 2;

		while(true){
			foreach(GameObject go in floors){
				Transform t = go.transform;
				DetermineFloorTile((int)t.position.x, (int)t.position.y);
			}
			yield return new WaitForSeconds(interval);

			if(Random.value < 0.1f)
				interval = Random.Range(1f,5f);
		}
	}

	private void DetermineFloorTile(int x, int y)
	{
		if(Player.Instance().GetLocation().x == x  &&
		   Player.Instance().GetLocation().y == y) return;

		//TODO: remove ifs and figure a better way to determine correct tile.
		if (Random.Range(0f, 1f) < chanceToSpawnDamageTile / 100f)
			floors[x, y].GetComponent<Tile>().TransformTo<DamageTile>();
		else if (Random.Range(0f, 1f) < chanceToSpawnPitfallTile / 100f)
			floors[x, y].GetComponent<Tile>().TransformTo<PitfallTile>();
	}

	void CreateRandomWalls(int chance)
	{
		foreach (GameObject floor in floors) {
			if (Random.Range(0, 101) < chance)
				Instantiate(wallPrefab, floor.transform.position, Quaternion.identity);
		}
	}

	public List<Wall> GetWallsInRadius(int radius, GameObject center)
	{

		Vector3 position = new Vector3((int)Mathf.Clamp(center.transform.position.x, 0f, width),
										(int)Mathf.Clamp(center.transform.position.y, 0f, height),
										0);

		List<Wall> resultList = new List<Wall>();

		int minX = Mathf.Max(0, (int)position.x - radius / 2);
		int maxX = Mathf.Min((int)position.x + radius / 2, width - 1);
		int minY = Mathf.Max(0, (int)position.y - radius / 2);
		int maxY = Mathf.Min((int)position.y + radius / 2, height - 1);

		for (int i = minX; i <= maxX; i++) {
			for (int j = minY; j <= maxY; j++) {
				resultList.Add(walls[i, j].GetComponent<Wall>());
			}
		}
		return resultList;
	}

	public bool AccessibleLocation(Vector2 location)
	{

		if ((int)location.x < 0 || (int)location.y < 0) return false;
		if ((int)location.x >= width || (int)location.y >= height) //endtile
			return location == GetEndLocation();

		return walls[(int)location.x, (int)location.y].renderer.enabled == false;
	}

	private void PunchFirstPathThruMaze(Vector2 startPos, Vector3 endPos)
	{
		int length = Random.Range(2, width - 2);

		for (int i = (int)startPos.x + 1; i <= length; i++){
			walls[i, (int)startPos.y].renderer.enabled = false;
			if(i == 1)
				floors[i, (int)startPos.y].GetComponent<Tile>().TransformTo<BlinkTriggerTile>();
		}
		for (int i = (int)Mathf.Min(startPos.y, endPos.y); i < (int)Mathf.Max(startPos.y, endPos.y); i++)
			walls[length, i].renderer.enabled = false;
		for (int i = length; i < endPos.x; i++)
			walls[i, (int)endPos.y].renderer.enabled = false;
	}

	private bool IsPlayerInLocation(Vector3 location){
		return IsPlayerInLocation(new Vector2(location.x,location.y));
	}

	private bool IsPlayerInLocation(Vector2 location){
		return (location.x == Player.Instance().transform.position.x &&
		        location.y == Player.Instance().transform.position.y);
	}

	public void ActivateRandomFloors(){
		StartCoroutine(RandomizeFloorTiles());
	}

	public void ActivateBlinkingWalls(){
		foreach(GameObject go in walls){
			go.GetComponent<Wall>().TransformTo<BlinkingWall>();
			if(!IsPlayerInLocation(go.transform.position))
				go.renderer.enabled = false;
		}
	}

	public Vector2 GetEndLocation(){ 
		return new Vector2(endTile.transform.position.x,endTile.transform.position.y);
	}
}
