using UnityEngine;
using System.Collections;

public class ButtonAllowForBuild : MonoBehaviour {
	#region Public Variables
	public GameObject objectToBuild;
	#endregion

	#region Private Variables
	private int cost;
	private Color originalColor;
	private DetectTouches detection;
	private BuildManager buildManager;
	private ResourceManager resourceManager;
	#endregion


	// Use this for initialization
	void Start () 
	{
		//Set references
		resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
		cost = objectToBuild.GetComponent<UnitStatScript>().cost;
		detection = GameObject.Find("Main Camera").GetComponent<DetectTouches>();
		buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();
		originalColor = this.gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		//If the Game is Paused
		if (SceneManager.isPaused) 
		{
			//Stop Updating
			return;
		}
		//If the player does not have enough resources to build the unit
		if (resourceManager.resource < objectToBuild.GetComponent<UnitStatScript>().cost) 
		{
			//30% brightness
			originalColor.r = .3f;
			originalColor.g = .3f;
			originalColor.b = .3f;
		}
		//If the player does have the resources to purchase the unit and it is
		//selected (Active State)
		else if (buildManager.objectToBuild.Contains(objectToBuild)) 
		{
			//70% brightness
			originalColor.r = .7f;
			originalColor.g = .7f;
			originalColor.b = .7f;
		}
		//If the player does have enough resources to purchase the unit and
		//it is not selected (Default State)
		else
		{
			//100% brightness
			originalColor.r = 1f;
			originalColor.g = 1f;
			originalColor.b = 1f;
		}
		//apply the above changes
		this.gameObject.GetComponent<Renderer>().material.color = originalColor;
		//If we are in debug mode
		if (detection.debugMode) 
		{
			//If this object is selected
			if (buildManager.objectToBuild.Contains(objectToBuild)) 
			{
				//Make me magenta
				this.GetComponent<Renderer>().material.color = Color.magenta;
			}
			else
			{
				//if not go back to default
				this.GetComponent<Renderer>().material.color = originalColor;
			}
		}

	
	}
	void Clicked()
	{
		if (resourceManager.resource >= objectToBuild.GetComponent<UnitStatScript>().cost) 
		{
			buildManager.SelectOrDeselectObjectToBuild(objectToBuild,cost);
		}


	}

}
