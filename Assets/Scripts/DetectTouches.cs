using UnityEngine;
using System.Collections;

public class DetectTouches : MonoBehaviour
{
    #region Public Variables
    public bool debugMode = false;
	public bool isStunned = false;
	public float stunDuration;
    #endregion

    // Use this for initialization
	void Start () 
	{
		//Forces the Game Screen into Landscape Left Orientation for Moblie Devices
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		//If The Game is not paused
		if (!SceneManager.isPaused) {
            //Check if I am stunned
			if (isStunned) 
			{
                
				stunDuration -= Time.deltaTime;
				if (stunDuration <= 0) 
				{
                    //No longer stunned!
					isStunned = false;
				}
                //return out of update(players who are stunned cannot click objects!)
				return;
			}

			Ray ray;
			RaycastHit hit;

			//If Left Clck(or touch) down
			if (Input.GetMouseButtonDown(0)) 
			{
				//Shoots a Ray from the point of touch
				ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				//If the ray collides with anything between the point shot and infinity
				if (Physics.Raycast(ray,out hit,Mathf.Infinity)) 
				{
					//If in debug mode
					if (debugMode) 
					{
						//Tell user what it collided with
						Debug.Log("Left Click Ray Hit: " + hit.collider.gameObject.name);
					}
					//Tell the Collider object to execute all "Clicked" functions
					hit.transform.SendMessage("Clicked",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
			//On Left Click up or release of touch
			if (Input.GetMouseButtonUp(0)) 
			{
				//Shoot a Ray from the point it was released
				ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				//If the ray collides with anything between the point shot and infinity
				if (Physics.Raycast(ray,out hit,Mathf.Infinity)) 
				{
					//If in debug mode
					if (debugMode) 
					{
						//Tell the user what it collided with
						Debug.Log("Released: Ray Hit: " + hit.collider.gameObject.name);
					}
					//Tell the Collider object to execute all "Release" functions
					hit.transform.SendMessage("Release",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
			//On Right Clicked
			if (Input.GetMouseButtonDown(1)) 
			{
				//Shoot a Ray from the mouse 
				ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				//If the ray collides with anything between the point shot and infinity
				if (Physics.Raycast(ray,out hit,Mathf.Infinity)) 
				{
					//If in debug mode
					if (debugMode) 
					{
						//Tell the user what object was Right Clicked
						Debug.Log("Right Click Ray Hit: " + hit.collider.gameObject.name);
					}
					//Tell the Collider object to execute all "RightClicked" Functions
					hit.transform.SendMessage("RightClicked",hit.point,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	
	}
}
