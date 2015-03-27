using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChestScript : MonoBehaviour {
	#region Public Variables
	public List<GameObject> objectsToSpawn;
	public float spawnTime = 5f;
	public int numberOfObjectsToSpawn = 1;
	public float chestOpenDelay;
	#endregion

	#region Private Variables
	public bool shouldSpawn = true;
	public bool isOpen;
	private float chestOpenCounter;
	private float counter;
	private float chestCloseCounter;
	#endregion

	// Use this for initialization
	void Start () 
	{
		chestOpenCounter = chestOpenDelay;
		//Set the Counter
		chestCloseCounter = chestOpenDelay;
		counter = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		//If game is paused
		if (SceneManager.isPaused) 
		{
			//stop updating
			return;
		}


		//if the counter is at or below zero

		if (counter <= 0) 
		{
			//if the chest is opening

			if (shouldSpawn) {
				if (this.gameObject.GetComponent<Animator>() != null) 
				{
					isOpen = true;
					this.gameObject.GetComponent<Animator>().SetBool("Open",isOpen);
				}	
				if (chestOpenCounter <= 0) {
					//Spawn X number of objects at the chests position
					for (int i = 0; i < numberOfObjectsToSpawn; i++) {
						Instantiate(objectsToSpawn[Random.Range(0,objectsToSpawn.Count)],this.gameObject.transform.position - new Vector3(0,0,4),Quaternion.identity);
					}
					CallForHint();
					shouldSpawn = false;
				}
				else 
				{
					chestOpenCounter-=Time.deltaTime;
				}


			}
			//once chest is fully open
			if (chestCloseCounter <= 0) {

				if (this.gameObject.GetComponent<Animator>() != null) 
				{
					isOpen = false;
					this.gameObject.GetComponent<Animator>().SetBool("Open",isOpen);
				}
				//Reset the counter
				counter = spawnTime;
				chestOpenCounter = chestOpenDelay;
				shouldSpawn = true;
				chestCloseCounter = chestOpenDelay;


			}
			else 
			{
				chestCloseCounter -= Time.deltaTime;
			}

		}
		//if the counter is not below zero
		else
		{
			//remove the time pased from the counter
			counter -= Time.deltaTime;
		}
	
	}
	public void CallForHint()
	{
		if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>() != null) 
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>().Bubble();
		}
	}

}
