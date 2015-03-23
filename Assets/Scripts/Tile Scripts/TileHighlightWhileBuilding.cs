using UnityEngine;
using System.Collections;

public class TileHighlightWhileBuilding : MonoBehaviour {
	private Color originalColor;
	private BuildManager buildManager;
	// Use this for initialization
	void Start () {
		//Store a reference to the build manager
		buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();
		//Store the reference to the original color
		originalColor = this.gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		//If the game is in the selection screen
		if (SceneManager.isInSelection) 
		{
			//Change the alpha of the tile's color to zero so the lanes aren't shown when selection is taking place.
			originalColor.a = 0f;
			this.gameObject.GetComponent<Renderer>().material.color = originalColor;
			
		}
		//If the game is paused
		if (SceneManager.isPaused) 
		{
			//Exit out of the update function
			return;
		}
		//if the build manager has an object to build
		if (buildManager.objectToBuild.Count > 0) 
		{
			//Change the alpha of the tile's color to a less transparent value to indicate you are about to build something
			originalColor.a = .15f;
			this.gameObject.GetComponent<Renderer>().material.color = originalColor;
			
		}
		else
		{
			//Change the alpha of the tile's color to a more transparent value to indicate you are not abouit to build anything
			originalColor.a = .025f;
			this.gameObject.GetComponent<Renderer>().material.color = originalColor;
		}
	
	}

}
