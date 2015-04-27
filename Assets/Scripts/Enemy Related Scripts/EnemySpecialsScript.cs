using UnityEngine;
using System.Collections;

public class EnemySpecialsScript : MonoBehaviour {

	public bool canExplodeOnHit;
	public int explosionDamage;

	public bool canStun;
	public int stunChance;
	public int stunDuration;

	public bool canSpawnEnemiesOnDeath;
	public int numberOfEnemiesToSpawn;
	public GameObject enemyToSpawn;

	private EnemyStatScript sR;
	// Use this for initialization
	void Start () {
		sR = gameObject.GetComponent<EnemyStatScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Attack ()
	{
		//If my attack cooldown counter is 0 or less
		if (sR.AttackCounter <= 0)
		{
			//If I have a target
			if (sR.Targets.Count > 0)
			{
				//Damage Target
				sR.Targets[0].GetComponent<UnitStatScript>().DeltaHealth(-sR.damage);
				//Should I go boom?
				if (canExplodeOnHit) 
				{
					Debug.Log(sR.Targets[0].GetComponent<UnitStatScript>().GetHealth());
					sR.Targets[0].GetComponent<UnitStatScript>().DeltaHealth(-explosionDamage);
					Debug.Log(sR.Targets[0].GetComponent<UnitStatScript>().GetHealth());
					Destroy(this.gameObject);
				}
				if (canStun) 
				{
					if (Random.Range(1,101) > stunChance) 
					{
						sR.Targets[0].GetComponent<UnitStatScript>().stunDuration = stunDuration;
					}	
				}
				
			}
			//Reset my counter to my Cooldown
			sR.AttackCounter = sR.attackInterval;
			//If my target's health is below zero
			if (sR.Targets[0].GetComponent<UnitStatScript>().GetHealth() <= 0)
			{
				//Remove the target from my targets list
				sR.Targets.RemoveAt(0);
			}
		}
        else
        {
            sR.AttackCounter -= Time.deltaTime;
        }

	}
	public void OnDeath ()
	{
		if (canSpawnEnemiesOnDeath) 
		{
			for (int i = 0; i < numberOfEnemiesToSpawn; i++) 
			{
				GameObject fish = GameObject.Instantiate(enemyToSpawn,gameObject.transform.position+ new Vector3(.5f*i,0,0),enemyToSpawn.transform.rotation) as GameObject;
				fish.tag = gameObject.tag;
			}

		}
	}
}
