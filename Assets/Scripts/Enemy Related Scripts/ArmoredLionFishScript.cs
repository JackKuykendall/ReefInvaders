using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoredLionFishScript : MonoBehaviour 
{
	public GameObject attachment;
	public int percentageToLoseAttachment = 70;

	private float realPercentage;
    private Vector3 fallVector;
	private EnemyStatScript scriptRef;

	// Use this for initialization
	void Start () {
		//Sets up reference to the EnemyStatScript script attached to
		//the game object this script is attached to
		scriptRef = this.gameObject.GetComponent<EnemyStatScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Calculates the real percentage to lose the attachment
		realPercentage = (float)percentageToLoseAttachment/100;
		//Debug.Log(realPercentage);

		//Checks if this enemies's health percantage is lower than the percentage
		//to lose the game object.
		if ((float)scriptRef.Health/(float)scriptRef.maxHealth <= realPercentage) 
		{
            scriptRef.hasAttachment = false;
			//Have the game object start falling down
			//CONSIDER:Due to the parent/child connection, when the parent dies
			//the attachment will also die even if it is still on the screen
			//Fix:Unparent the attachment at this point
            attachment.transform.position += new Vector3(0, -Time.deltaTime * 2, 0);
		}
		
	}
	//Function that deactivates the Attachment object.
	void DeActivate()
	{
		attachment.SetActive(false);
	}



	
}

