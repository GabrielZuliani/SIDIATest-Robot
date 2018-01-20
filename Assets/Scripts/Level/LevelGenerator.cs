using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private int numberOfLanes = 5;
	[SerializeField] private float laneSize = 1.5f;
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private GameObject floorPrefab;
	[SerializeField] private int pathLength = 3;
	[SerializeField] private int wallHeight = 3;

	
	public int NumberOfLanes
	{
		get { return numberOfLanes; }
	}

	public float LaneSize
	{
		get { return laneSize; }
	}

	public int PathLength
	{
		get { return pathLength; }
	}
	
	public Transform Generate()
	{
		var parent = new GameObject ("Level Geometry").transform;

		var leftWall = Instantiate(wallPrefab, parent);
		leftWall.transform.localScale = new Vector3(WallWidth, laneSize * wallHeight, pathLength * laneSize);
		leftWall.transform.position = transform.position + WallOffset(true);
		leftWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(pathLength, wallHeight);

		var floor = Instantiate(floorPrefab, parent);
		floor.transform.localScale = new Vector3(numberOfLanes * laneSize, laneSize, pathLength * laneSize);
		floor.transform.position = transform.position + FloorOffset();
		floor.GetComponent<Renderer>().material.mainTextureScale = new Vector2(numberOfLanes, pathLength);
		
		var rightWall = Instantiate(wallPrefab, parent);
		rightWall.transform.localScale = new Vector3(WallWidth, laneSize * wallHeight, pathLength * laneSize);
		rightWall.transform.position = transform.position + WallOffset(false);
		rightWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(pathLength, wallHeight);

		return parent;
	}
	

	private Vector3 WallOffset(bool isLeft)
	{
		var xSign = isLeft ? -1 : 1; 
		return new Vector3(xSign * (numberOfLanes /2 * laneSize + WallWidth/2 +  numberOfLanes % 2 * laneSize) , wallHeight * laneSize / 2, (pathLength/2 - 1) * laneSize); 
	}
	
	private Vector3 FloorOffset()
	{
		 return new Vector3(0,laneSize / 2, (pathLength/2 - 1) * laneSize); 
	}

	private float WallWidth
	{
		get { return laneSize / 2; }
	}
}
