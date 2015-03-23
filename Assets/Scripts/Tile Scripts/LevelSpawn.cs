using UnityEngine;
using System.Collections;

public class LevelSpawn : MonoBehaviour {
	public GameObject tile;
	public int length = 7;
	public int width = 5;
	public float xGridOffset;
	public float yGridOffset;

	private GameObject gridObject;

	private int[,] grid;
	// Use this for initialization
	void Start () 
	{
		gridObject = GameObject.Find("Grid");
		grid = new int[width,length];
		for (int i = 0; i < length; i++) 
		{
			for (int x = 0; x < width; x++) 
			{

				GameObject tileInstance = Instantiate(tile,new Vector3(i,x,0),tile.transform.rotation) as GameObject;
				tileInstance.tag = "Tile";
				tileInstance.GetComponent<AllowForBuildOnTile>().lane = x;
				tileInstance.transform.parent = gridObject.transform; 
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.isPaused) 
		{
			return;
		}
		gridObject.transform.position = new Vector3(xGridOffset,yGridOffset,0);
	}
}
