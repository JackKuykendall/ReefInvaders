using UnityEngine;
using System.Collections;

public class SpecialAttackScript : MonoBehaviour {
	UnitStatScript sR;

    public int abilitySound;
    public int volume;
    public int pitch;

    public GameObject projectile;
	public bool canSlow;
	public float slowPercentage;
	public float slowDuration;

	public bool canStun;
	public float stunChance;
	public float stunDuration;

	public bool canKnockBack;
	public float knockbackChance;
	public float knockbackDistance;

	public bool canChangeAttackInterval;
	public float attackIntervalMin;
	public float attackIntervalMax;

	public bool canHealOnHit;
	public int healthGainedOnHit;
	
	private float maxDistance = 6f;
	private float distanceModifier;

	// Use this for initialization
	void Start () {
		sR = this.gameObject.GetComponent<UnitStatScript>();
		distanceModifier = (attackIntervalMax-attackIntervalMin)/maxDistance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Attack()
	{

		if (sR.Counter <= 0)
		{
			if (canChangeAttackInterval) {
				if (sR.Targets.Count > 0) 
				{
					//Debug.Log("ChangeAttackInterval" + sR.attackInterval);
					sR.attackInterval = attackIntervalMin + (Mathf.Abs(gameObject.transform.position.x - sR.Targets[0].transform.position.x)*distanceModifier);
					//Debug.Log("NewAttackInterval" + sR.attackInterval);
				}
			}
			sR.Counter = sR.AttackInterval;
            AudioManager.PlayAbilitySound(abilitySound, volume, pitch);
			GameObject proj = Instantiate(sR.projectile, new Vector3(this.transform.position.x + .3f,this.transform.position.y,this.transform.position.z+.05f), sR.projectile.transform.rotation) as GameObject;
			Projectile projscript = proj.GetComponent<Projectile>();
			projscript.OriginFish = this.gameObject;
			projscript.damage = sR.damage;
			//Tell the projectile what it can/should do
			projscript.SetSpecials(canSlow,canStun,canKnockBack,canHealOnHit);
			if (canSlow) {
				projscript.SetSlow(slowPercentage, slowDuration);
			}
			if (canStun) {
				projscript.SetStun(stunChance,stunDuration);
			}
			if (canKnockBack) 
			{
				projscript.SetKnockback(knockbackChance,knockbackDistance);	
			}
			if (canHealOnHit) 
			{
				projscript.SetHeal(healthGainedOnHit);
			}


			if (sR.Animator != null) {
				sR.Animator.SetBool("Attack", sR.IsAttacking = false);
			}

		}
		else
		{
			sR.Counter -= Time.deltaTime;
		}
		
	}
}
