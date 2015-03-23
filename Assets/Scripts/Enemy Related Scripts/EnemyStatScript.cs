using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStatScript : MonoBehaviour
{
    #region Public Variables
    public Texture tex;
    public int isHitSound;
    public int isHitWASound;
    public float volume;
    public float pitch;
    public string description;
    public float moveSpeed = .5f;
    public int damage = 10;
    public int maxHealth = 100;
    public float attackInterval = 1.5f;
    public int reward = 10;
    public GameObject attachment;
    public float slowPercentage = .7f;
    public bool hasAttachment;
    [HideInInspector]
    public bool isSlowed = false;
	[HideInInspector]
	public bool isStunned = false;
	public bool willBellyUp;

    public List<GameObject> Targets;
    #endregion

    #region Private Variables
    private bool isAttacking;
    private bool canDamageReef = true;
    private bool isDead = false;
    private Color color;
    private float healthTicks;
    private int health;
    private float attackCounter = 0;
    private Vector3 direction;
    private bool shouldMove = true;
    private SceneManager sceneManager;
    private ResourceManager resourceManager;
    private GameObject[] array;
    private Animator animator;
	private float stunDuration;
	private float slowDuration;
    #endregion


	public float StunDuration
	{
		get
		{
			return stunDuration;
		}
		set
		{
			stunDuration = value;
		}
	}
	public float SlowDuration
	{
		get
		{
			return slowDuration;
		}
		set
		{
			slowDuration = value;
		}
	}
	public float AttackCounter
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





    // Use this for initialization
    void Start()
    {
        //Sets References
        if (this.gameObject.GetComponent<Animator>() != null)
        {
            animator = this.gameObject.GetComponent<Animator>();
        }
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        color = this.gameObject.GetComponent<Renderer>().material.color;
        //Calculates how much health a Tick should be
        healthTicks = 1 / (float)maxHealth;
        //Store Health in a Private Variable
        health = maxHealth;
        //Sets the enemies moving Direction(mostly used to avoid creating new vectors every frame)
        direction = new Vector3(-1, 0, 0);
        //Instantiates a new list(overkill)
        Targets = new List<GameObject>();
    }

    // Update is called once per frame
	public void CheckState()
	{
		if (stunDuration > 0) 
		{
			isStunned = true;
		}
		if (slowDuration > 0) 
		{
			isSlowed = true;
		}
	}
    void Update()
    {
		CheckState();
        //If game is paused
        if (SceneManager.isPaused)
        {
			this.GetComponent<Rigidbody>().isKinematic = true;
            //stop updating
            return;
        }
		else 
		{
			this.GetComponent<Rigidbody>().isKinematic = false;
		}
		if (isStunned) 
		{
			stunDuration -= Time.deltaTime;
			if (stunDuration <= 0) 
			{
				isStunned = false;
			}
			if (animator != null) 
			{
				animator.SetBool("Attack", isAttacking = false);
			}
			return;
		}
        //If the object isn't dead(a belly up fish is considered dead)
        if (!isDead)
        {
            if (canDamageReef)
            {
                //If the object has moved off screen left
                if (this.gameObject.transform.position.x <= -2)
                {
                    //Damage the Reef
                    sceneManager.GetComponent<SceneManager>().DamageReef();
					OnDeath();
                    canDamageReef = false;
                }

            }
        }

        //If this object has a Target
        if (Targets.Count > 0)
        {
            //Then attack
            Attack();
            if (animator != null)
            {
                animator.SetBool("Attack", isAttacking = true);
            }
        }
        //If it does not have an target
        else
        {
            //Tell itself that it should be moving
            shouldMove = true;
            if (animator != null)
            {
                animator.SetBool("Attack", isAttacking = false);
            }
        }
        //If this object should be moving
        if (shouldMove)
        {
            //If this object is slowed
            if (isSlowed)
            {

                //move at movespeed modified by show percentage
                transform.position += direction * moveSpeed * slowPercentage * Time.deltaTime;
				//slow Duration handler
				slowDuration -= Time.deltaTime;
				if (slowDuration <= 0) 
				{
					isSlowed = false;
				}

                
            }
            //If the object is not slowed 
            else
            {
                //move at movespeed
                transform.position += direction * moveSpeed * Time.deltaTime;
            }

        }
        //If the object is dead
        if (isDead)
        {
            //If the object is above the top of the screen
            if (this.transform.position.y > 6)
            {
                //Destroy myself
                AudioManager.LFDeath();
                OnDeath();
            }
			else 
			{
				//shuuush ignore the hardcoded 10 you don't see anything!
				this.transform.position = new Vector3(this.transform.position.x,SceneManager.Lerp(this.transform.position.y,10f,.2f*Time.deltaTime),this.transform.position.z);
			}
        }

    }
    //Function to check Collisions
    void OnTriggerEnter(Collider col)
    {
        //If I collided with a Player Unit
        if (col.CompareTag("PlayerUnit"))
        {
            //Set the colider player unit to a target of mine
            Targets.Add(col.gameObject);
            //Tell myself I shouldnt move
            shouldMove = false;
        }
    }

    //Function to change the health of this object
    public void DeltaHealth(int deltaHealth)
    {
        //Modify by the amount specified
        health += deltaHealth;

        if (hasAttachment)
        {
            AudioManager.PlayDamagedSound(isHitWASound, volume, pitch);
        }
        else
        {
            AudioManager.PlayDamagedSound(isHitSound, volume, pitch);
        }
        //If my health is above my max health
        if (health > maxHealth)
        {
            //reset my health to max
            health = maxHealth;
        }

		#region setHealthColorState
		//Set My Health State
		if (isSlowed) 
		{
			color.r = 1f - (health * (healthTicks * .5f));
			color.g = 0f + (health * (healthTicks * .9f));
			color.b = -.25f + (health * (healthTicks * .75f));
		}
		else
		{
			color.r = 1f;
			color.g = 0f + (health * healthTicks);
			color.b = 0f + (health * healthTicks);
		}
		this.gameObject.GetComponent<Renderer>().material.color = color;
		#endregion


        //If my health is below or equal Zero
        if (health <= 0)
        {
            AudioManager.LFDeath();
            //if I am not belly Up
            if (!isDead)
            {
                //Store all player units
                array = GameObject.FindGameObjectsWithTag("PlayerUnit");
                //if the enemy spawner says player should get money on killing a unit
                if (GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().shouldRewardOnKill)
                {
                    //Give the player reward money
                    resourceManager.DeltaResourceTG(reward, transform.position);
                }
                //Random a number between 1-100
                int x = Random.Range(0, 100);
                //If that number is above 70
                if (x > 70 || willBellyUp)
                {
					//Calls For a Hint if this is a tutorial Level
					CallForHint();
                    //Flip myself bellyup, turn my gravity on, consider me dead
                    this.gameObject.transform.Rotate(0, 0, 180);
                    //this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    isDead = true;
                    //Change my tag to a DeadFish so towers wont consider it a target
                    this.gameObject.tag = "DeadFish";
                    //If I have an attachment
                    if (attachment != null)
                    {
                        //Turn it off
                        attachment.SetActive(false);
                    }
                }
                //If I shouldn't go bellyup
                else
                {
                    //Kill me
                    OnDeath();
                }
            }


        }
    }
    //Function that handles the attacking
    public virtual void Attack()
    {
		if (isDead) 
		{
			if (animator != null)
			{
				animator.SetBool("Attack", isAttacking = false);
			}
			return;
		}
		if (this.gameObject.GetComponent<EnemySpecialsScript>() !=  null) 
		{
			this.gameObject.GetComponent<EnemySpecialsScript>().Attack();
			return;
		}
        //If my attack cooldown counter is 0 or less
        if (attackCounter <= 0)
        {
            //If I have a target
            if (Targets.Count > 0)
            {
                //Damage Target

                Targets[0].GetComponent<UnitStatScript>().DeltaHealth(-damage);

            }
            //Reset my counter to my Cooldown
            attackCounter = attackInterval;
            //If my target's health is below zero
            if (Targets[0].GetComponent<UnitStatScript>().GetHealth() <= 0)
            {
                //Remove the target from my targets list
                Targets.RemoveAt(0);
            }
        }
        //If my attack counter is not less than or equal to zero
        else
        {
            //subtract the time passed from the counter
            attackCounter -= Time.deltaTime;
        }
    }
    //Health Property
    public int Health
    {
        get
        {
            return health;
        }
    }

    //Function that is called when it is clicked
    void Clicked()
    {
        //If I am bellyup
        if (isDead)
        {
            //for each Player Unit
            foreach (GameObject attacker in GameObject.FindGameObjectsWithTag("PlayerUnit"))
            {
                //If the attackers target list contains me
                if (attacker.GetComponent<UnitStatScript>().Targets.Contains(this.gameObject))
                {
                    //remove me
                    attacker.GetComponent<UnitStatScript>().Targets.Remove(this.gameObject);

                }
            }
            //Reward the player
            resourceManager.DeltaResourceTG(reward, transform.position);
            //Destroy myself
			OnDeath();
        }
    }
	public void CallForHint()
	{
		if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>() != null) 
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialScript>().LionFish();
		}
	}
	public virtual void OnDeath()
	{
		if (this.gameObject.GetComponent<EnemySpecialsScript>() != null) 
		{
			this.gameObject.GetComponent<EnemySpecialsScript>().OnDeath();
		}
		Destroy(this.gameObject);
	}

}
