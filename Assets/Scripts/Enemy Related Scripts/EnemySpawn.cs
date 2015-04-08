using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {

	//public GameObject enemy;
	public float timeForFirstSpawn = 5f;
	public GameObject[] lionFish;
	public List<int> enemiesByIndex;
	public List<GameObject> enemies;
	public List<float> spawnTimes;
	//public float timeBetweenSpawnsInSeconds = 2f;


	private int fishToSpawn;
	public bool shouldRewardOnKill = false;

	private float timeForSpawn;
	private LevelSpawn gridSpawner;
	private int spawnedFish = 0;
	private float counter = 0;
	private Vector3[] spawnPoints;
	private bool hasSpawned = false;
	private string[] tags;
	private bool doneSpawning = false;
	private SceneManager sceneManager;
	// Use this for initialization
	void Start () 
	{
		timeForSpawn = timeForFirstSpawn;
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
		gridSpawner = GameObject.Find("LevelSpawner").GetComponent<LevelSpawn>();
		tags = new string[5];
		//Set Tags
		for (int i = 0; i < tags.Length; i++) 
		{
			tags[i] = "Lane" + (i + 1);
			
		}
		//Set Spawn Points
		spawnPoints = new Vector3[5];
		for (int i = 0; i < 5; i++) 
		{
			spawnPoints[i] = new Vector3(8.5f + gridSpawner.xGridOffset,i + gridSpawner.yGridOffset,-.15f);
		}
		//If we are spawning enemies by their index
		if (enemiesByIndex.Count > 0) 
		{
			fishToSpawn = enemiesByIndex.Count;
		}
		//or if we are spawning them by the gameobject 
		else 
		{
			fishToSpawn = enemies.Count;	
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(counter);
		if (SceneManager.isPaused) 
		{
			return;
		}
		if (spawnedFish < fishToSpawn) 
		{
			if (counter >= timeForSpawn) 
			{
				if (!hasSpawned) 
				{


					hasSpawned = true;
					counter = 0;
					SpawnEnemy();
					++spawnedFish;
					CallForHint();
					//Debug.Log(spawnedFish);
					if (spawnTimes.Count > 0) {
						timeForSpawn = spawnTimes[0];
						spawnTimes.RemoveAt(0);
					}


				}

			}
			else
			{
				counter += Time.deltaTime;
			}
		}
		else 
		{
			doneSpawning = true;
		}
		if (doneSpawning) 
		{
			int x = 0;
			for (int i = 1; i <= 5; i++) 
			{
				if (GameObject.FindGameObjectsWithTag("Lane" + i).Length > 0) 
				{
					++x;
				}

			}
			if (x == 0) 
			{
				sceneManager.hasWon = true;
			}
	
		}

	
	}
	public void SpawnEnemy()
	{
        AudioManager.Swimming();
		int x = Random.Range(0,5);
		GameObject enemyInstance;
		//If we are spawning by index
		if (enemiesByIndex.Count > 0) {
			//spawn a lionfish by it's index and save it as an instance
			enemyInstance = Instantiate(lionFish[enemiesByIndex[0]],spawnPoints[x],lionFish[enemiesByIndex[0]].transform.rotation) as GameObject;
			enemiesByIndex.RemoveAt(0);
		}
		//If we are spawning by the game object
		else {
			//spawn whatever lionfish is next in the list and then save it as an instance
			enemyInstance = Instantiate(enemies[0],spawnPoints[x],enemies[0].transform.rotation) as GameObject;
			enemies.RemoveAt(0);
		}
		//Set the tag of the fish so that the allies know when to attack
		enemyInstance.tag = tags[x];
		hasSpawned = false;	
	}
	public void CallForHint()
	{
		if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>() != null) 
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>().Fish();
		}
	}
}
