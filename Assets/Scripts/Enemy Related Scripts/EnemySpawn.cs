using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {

	//public GameObject enemy;
	public float timeForFirstSpawn = 5f;
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
		for (int i = 0; i < tags.Length; i++) 
		{
			tags[i] = "Lane" + (i + 1);
			
		}
		spawnPoints = new Vector3[5];
		for (int i = 0; i < 5; i++) 
		{
			spawnPoints[i] = new Vector3(8.5f + gridSpawner.xGridOffset,i + gridSpawner.yGridOffset,-.15f);
		}
		fishToSpawn = enemies.Count;
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
		GameObject enemyInstance = Instantiate(enemies[0],spawnPoints[x],enemies[0].transform.rotation) as GameObject;
		enemyInstance.tag = tags[x];
		enemies.RemoveAt(0);
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
