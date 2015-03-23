using UnityEngine;
using System.Collections;

public class OctopusSlow : CooldownTimerScripts {
    public AudioClip ink;
	public ParticleSystem particleSys;
	public GameObject projectile;
	[HideInInspector]
	// Use this for initialization
	void Start () {
	
	}

    public override void Update()
    {
        base.Update();
    }

	public void Clicked()
	{
		if (canFire) 
		{
            GetComponent<AudioSource>().PlayOneShot(ink);
			Instantiate(particleSys,this.gameObject.transform.position - new Vector3(0,0,1),particleSys.transform.rotation);
			Instantiate(projectile,this.gameObject.transform.position,projectile.transform.rotation);
			canFire = false;
			counter = coolDown;
		}
	}
}
