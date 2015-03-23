using UnityEngine;
using System.Collections;

public class AllowForBuildOnTile : MonoBehaviour {
	private BuildManager buildManager;
	public int lane;
	// Use this for initialization
	void Start () 
	{
		//Locate Build Manager and set up reference to the script
		buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();

	}
	
	// Update is called once per frame
	void Update () {
		//Checks if the game is paused
		if (SceneManager.isPaused) 
		{
			//Stop executing the Update function
			return;
		}
	}
	//If the Object is told it is clicked
	void Clicked()
	{
		//If the build manager has an object to build
		if (buildManager.objectToBuild.Count > 0) 
		{
			//Tell the build manager to create a unit at this tile
			buildManager.BuildUnit(new Vector3((float)this.gameObject.transform.position.x,(float)this.gameObject.transform.position.y,(float)this.gameObject.transform.position.z-.1f),lane);
		}
	}
	//If the object is told there was a release above it
	void Release()
	{
		//If the build manager has an object to build
		if (buildManager.objectToBuild.Count > 0) 
		{
			//Tell the build manager to create a unit at this tile
			buildManager.BuildUnit(new Vector3((float)this.gameObject.transform.position.x,(float)this.gameObject.transform.position.y,(float)this.gameObject.transform.position.z-.1f),lane);
		}
	}
}
