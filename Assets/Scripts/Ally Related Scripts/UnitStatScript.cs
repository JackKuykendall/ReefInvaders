using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStatScript : MonoBehaviour
{
    public int projectileSound;
    public int isHit;
    public float volume;
    public float pitch;
	public bool isMelee = false;
	public float attackDelay = 1.5f;
    public string description;
    public int maxHealth = 100;
    private int health;
    public int damage = 15;
    [HideInInspector]
    public int range = 100;
    public float attackInterval = 1f;
    [HideInInspector]
    public bool canUpgrade = true;
    public bool isActive = false;
    [HideInInspector]
    public int lane;
    public int cost;
    public Texture tex;
    public GameObject projectile;
    public Color color;
    private float attackCounter;
    [HideInInspector]
    public List<GameObject> Targets;
    private bool shouldDestroy = false;
    private float healthTicks;
    private Animator animator;
    private bool isAttacking;

	public bool isStunned;
	public float stunDuration;

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.GetComponent<Animator>() != null)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
		//Sets the attacking variable
        isAttacking = false;
		//Creates a new Targets list
        Targets = new List<GameObject>();
		//Sets health
        health = maxHealth;
		//Calculates health ticks for the color changing
        healthTicks = 1 / (float)maxHealth;
		//Sets the First attack to sync with the animation
		attackCounter = attackDelay;
		//sets the color = initial color
        color = this.gameObject.GetComponent<Renderer>().material.color;
    }
	#region Properties
	public float Counter
	{
		get
		{
			return attackCounter;
		}
		set
		{
			attackCounter = value;
		}
	}
	public Animator Animator
	{
		get
		{
			return animator;
		}
	}
	public float AttackInterval
	{
		get
		{
			return attackInterval;
		}
	}
	public bool IsAttacking
	{
		get
		{
			return isAttacking;
		}
		set
		{
			isAttacking = value;
		}
	}
	#endregion

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.isPaused)
        {
            return;
        }
        if (!isActive)
        {
            color.r = 1f;
            color.g = 0f + ((float)health * healthTicks);
            color.b = 0f + ((float)health * healthTicks);
            this.GetComponent<Renderer>().material.color = color;
        }
		//If I am dead
        if (health <= 0)
        {
			//Find things in my lane and remove me from their targets list
            foreach (GameObject attacker in GameObject.FindGameObjectsWithTag("Lane" + (lane + 1)))
            {
                if (attacker.GetComponent<EnemyStatScript>().Targets.Contains(this.gameObject))
                {
                    attacker.GetComponent<EnemyStatScript>().Targets.Remove(this.gameObject);
                }
            }
			//Kill myself
            DestroySelf();
        }

		//If I am stunned break out of update
		if (stunDuration > 0) 
		{
			if (isStunned != true) 
			{
				isStunned = true;
			}
			stunDuration -= Time.deltaTime;
			return;
		}
		else 
		{
			isStunned = false;
		}

		//Resets the fishes' targets every frame of the game OPTIMIZE(Only do this if there is a change in fish (death or spawn))
        Targets.Clear();
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Lane" + (lane + 1)))
        {
            if (target.transform.position.x >= this.gameObject.transform.position.x && !Targets.Contains(target))
            {
				//if I am melee
				if (isMelee) 
				{
					//if the enemy is NOT in melee range
					if (Mathf.Abs(target.gameObject.transform.position.x - this.gameObject.transform.position.x) >= 1.2f) 
					{
						//Dont bother adding it
						continue;
					}
				}
				//otherwise add it
                Targets.Add(target);
            }
        }
		//if I have a target
        if (Targets.Count > 0)
        {
			//If I have an animator
            if (animator != null)
            {
				//Tell my animator that I am attacking
                animator.SetBool("Attack", isAttacking = true);
            }
			//Attack!
			//If this fish has a special attack it it handled in the attack function
            Attack();
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("Attack", isAttacking = false);
            }

        }

    }
	//Added incase we have healers/buffs
	/// <summary>
	/// Heals the Unit for the delta and increases the max health by that much
	/// </summary>
	/// <param name="deltaHealth">Delta health.</param>
    public void upgradeHealth(int deltaHealth)
    {
        if (deltaHealth < 0)
        {
            deltaHealth = deltaHealth * -1;
            maxHealth += deltaHealth;
            health += deltaHealth;
        }
        else
        {
            maxHealth += deltaHealth;
            health += deltaHealth;
        }
    }
	/// <summary>
	/// Deltas the health (insert negative number to deal damage).
	/// </summary>
	/// <param name="deltaHealth">Delta health.</param>
    public void DeltaHealth(int deltaHealth)
    {
        if(deltaHealth < 0)
        {
            AudioManager.PlayDamagedSound(isHit, volume, pitch);
        }
        health += deltaHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        shouldDestroy = true;
        //Destroy(this.gameObject);
    }
    public int GetHealth()
    {
        return health;
    }
    void Attack()
    {
		//If this game object has a special attack call it, otherwise attack normally.
		if (this.gameObject.GetComponent<SpecialAttackScript>() != null) 
		{
			this.gameObject.GetComponent<SpecialAttackScript>().Attack();
			//If this has a special attack, return out of the function after calling it, this prevents double attacking
			return;
		}
        if (attackCounter <= 0)
        {
            attackCounter = attackInterval;
            AudioManager.PlayProjectileSound(projectileSound, volume, pitch);
			GameObject proj = Instantiate(projectile, new Vector3(this.transform.position.x + .3f,this.transform.position.y,this.transform.position.z+.05f), projectile.transform.rotation) as GameObject;
            proj.GetComponent<Projectile>().damage = damage;
			proj.GetComponent<Projectile>().OriginFish = this.gameObject;
			if (animator != null) {
				animator.SetBool("Attack", isAttacking = false);
			}

        }
        else
        {
            attackCounter -= Time.deltaTime;
        }

    }
    void Clicked()
    {
        if (UnitManager.shouldSell)
        {
            GameObject.Find("ResourceManager").GetComponent<ResourceManager>().DeltaResourceTG(cost / 2, transform.position);
            health = 0;
            UnitManager.shouldSell = false;
        }
    }
    void LateUpdate()
    {
        if (shouldDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
