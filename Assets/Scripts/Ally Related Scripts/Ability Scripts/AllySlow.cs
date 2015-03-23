using UnityEngine;
using System.Collections;

public class AllySlow : CooldownTimerScripts {

    public int abilitySound;
    public float volume;
    public float pitch;

	public ParticleSystem particleSys;
	public GameObject projectile;

    //Boolean to see if the ally using this script is using a Particle System.
    public bool hasPS;
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
            //audio.PlayOneShot(ink);

            if (hasPS)
            {
                Instantiate(particleSys, this.gameObject.transform.position - new Vector3(0, 0, 1), particleSys.transform.rotation);
            }
            Instantiate(projectile, this.gameObject.transform.position, projectile.transform.rotation);
			canFire = false;
			counter = coolDown;
		}
	}
}
