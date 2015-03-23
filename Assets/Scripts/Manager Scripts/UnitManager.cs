using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour 
{
	public List<GameObject> SelectedUnits;
	private DetectTouches detection;
	public static bool shouldSell = false;
	// Use this for initialization
	void Start () 
	{
		shouldSell = false;
		detection = GameObject.Find("Main Camera").GetComponent<DetectTouches>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.isPaused) 
		{
			return;
		}
		if (detection.debugMode)
		{
			if (Input.GetKeyDown(KeyCode.K)) 
			{
				DestroyUnit();
			}
		}
	
	}
	public void SelectUnit(GameObject unit)
	{
		SelectedUnits.Clear();
		SelectedUnits.Add(unit);
	}
	public void DeselectUnit()
	{
		SelectedUnits.Clear();
	}
	private void DestroyUnit()
	{
		if (SelectedUnits.Count > 0) 
		{
			SelectedUnits[0].SendMessage("DestroySelf");
		}
	}
}
