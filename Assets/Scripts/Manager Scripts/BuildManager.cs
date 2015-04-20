using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour {
	#region Public Variables
	[HideInInspector]
	public List<GameObject> objectToBuild;
	#endregion


	#region Private Variables
	private int cost;
	private ResourceManager resourceManager;
	private bool previousPauseState;
	private bool currentPauseState;
	#endregion

	// Use this for initialization
	void Start () {
		//Sets the reference to the Resource Manager's Resource Manager script
		resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
		previousPauseState = currentPauseState;
		currentPauseState = SceneManager.isPaused;
		if (SceneManager.isPaused) 
		{
			objectToBuild.Clear();
		}
		if (previousPauseState == false && currentPauseState == true) 
		{
			objectToBuild.Clear();
		}
	
	}
	//Function to Build New Unit
	public void BuildUnit(Vector3 position, int lane)
	{
		//If the player has enough resources to build the unit
		if (resourceManager.hasResources(cost)) 
		{
			//Apply the cost of an object
			resourceManager.DeltaResource(-cost);

			//Creates a new unit and stores it, Then sets the new units lane
			GameObject UnitBuilt = Instantiate(objectToBuild[0],position,objectToBuild[0].transform.rotation) as GameObject;
			UnitBuilt.GetComponent<UnitStatScript>().lane = lane;
			//Tells the build manager to reset to default(essentially deselect the button)
			objectToBuild.Clear();
			cost = 0;
		}
	}
	//Function To Select A Unit to build
	public void SelectOrDeselectObjectToBuild(GameObject unit, int insertCost)
	{
		//If the player has stated there should be something to build and 
		//something is selected
		if (objectToBuild.Count > 0) 
		{
			//If unit to selecte is already selected
			if (objectToBuild[0] == unit) 
			{
				//reset to default values (Deselect)
				objectToBuild.Clear();
				cost = 0;
			}
			else
			{
				//Remove any Selection
				objectToBuild.Clear();
				//Add new selection and cost
				objectToBuild.Add(unit);
				cost = insertCost;
			}
		}
		else
		{
			//Add new selection and cost
			objectToBuild.Add(unit);
			cost = insertCost;
		}

	}
}
