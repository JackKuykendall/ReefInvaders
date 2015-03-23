using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed = 1.3f;
	public int damage;

	private bool canSlow;
	private float slowPercentage;
	private float slowDuration;

	private bool canStun;
	private float stunChance;
	private float stunDuration;

	private bool canKnockBack;
	private float knockBackChance;
	private float knockBackDistance;

	private bool canHealOnHit;
	private int healthGainedOnHit;

	private bool shouldDestroy = true;
	private float counter;
	private float lifeSpan = 8f;
	private Vector3 direction;

	public GameObject OriginFish
	{
		get;
		set;
	}


	public void SetSpecials(bool CanSlow,bool CanStun, bool CanKnockBack, bool CanHealOnHit)
	{
		canSlow = CanSlow;
		canStun = CanStun;
		canKnockBack = CanKnockBack;
		canHealOnHit = CanHealOnHit;
	}
	public void SetSlow(float percentage, float duration)
	{
		slowPercentage = percentage;
		slowDuration = duration;
		
	}
	public void SetStun(float percentageChance, float duration)
	{
		stunChance = percentageChance;
		stunDuration = duration;
		
	}
	public void SetKnockback(float percentageChance, float distance)
	{
		knockBackChance = percentageChance;
		knockBackDistance = distance;
		
	}
	public void SetHeal(int healthGained)
	{
		healthGainedOnHit = healthGained;
	}

	// Use this for initialization
	void Start () 
	{
		direction = new Vector3(1,0,0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.isPaused) 
		{
			return;
		}
		if (counter >= lifeSpan) 
		{
			Destroy(this.gameObject);
		}
		else 
		{
			counter += Time.deltaTime;
		}
		this.gameObject.transform.position += direction * moveSpeed * Time.deltaTime; 
	
	}
	void OnTriggerEnter(Collider col)
	{
		for (int i = 1; i < 6; i++) 
		{
			//If I colide with anything enemy
			if (col.CompareTag("Lane" + i)) 
			{
				EnemyStatScript sr = col.GetComponent<EnemyStatScript>();
				if (canSlow) 
				{
					sr.slowPercentage = slowPercentage;
					sr.SlowDuration = slowDuration;
				}
				if (canStun) 
				{
					if (Random.Range(0,100) > 100-stunChance) 
					{
						sr.StunDuration = stunDuration;
					}
				}
				if (canHealOnHit) 
				{
					if (OriginFish != null) 
					{
						OriginFish.GetComponent<UnitStatScript>().DeltaHealth(healthGainedOnHit);
					}
				}
				//TODO Impliment Knockback
				sr.DeltaHealth(-damage);

				if (shouldDestroy) 
				{
					Destroy(this.gameObject);	
				}

			}
		}

	 }
}
