using UnityEngine;
using System.Collections;

public class UnitAllowForSelection : MonoBehaviour {

	private UnitManager manager;
	private DetectTouches detection;
	private Color originalColor;
	// Use this for initialization
	void Start () 
	{
		detection = GameObject.Find("Main Camera").GetComponent<DetectTouches>();
		manager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
		originalColor = gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.isPaused) 
		{
			return;
		}
		if (detection.debugMode) 
		{
			if (manager.SelectedUnits.Contains(this.gameObject)) 
			{
				this.GetComponent<Renderer>().material.color = Color.magenta;
			}
			else
			{
				this.GetComponent<Renderer>().material.color = originalColor;
			}
		}
	}
	void Clicked()
	{
		manager.SelectUnit(this.gameObject);
	}
}
