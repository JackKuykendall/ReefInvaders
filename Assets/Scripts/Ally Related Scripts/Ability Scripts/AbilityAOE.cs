using UnityEngine;
using System.Collections;

public class AbilityAOE : CooldownTimerScripts
{

    //public AudioClip shock;
	public int damage;
    public ParticleSystem particleSys;
    //private bool isAttacking = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void Clicked()
    {
        if (canFire)
        {
			if (particleSys != null) 
			{
				particleSys.GetComponent<AbilityAOE_PS>().damage = damage;
				Instantiate(particleSys, this.gameObject.transform.position, particleSys.transform.rotation);

			}
            canFire = false;
            counter = coolDown;
        }

    }
}
