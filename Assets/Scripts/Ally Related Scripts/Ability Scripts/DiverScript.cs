using UnityEngine;
using System.Collections;

public class DiverScript : CooldownTimerScripts {

    public AudioClip harpoon;
    public GameObject projectile;
    //private bool hasDrawnBlood = false;

    
	// Use this for initialization
	void Start () {
	
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
            GetComponent<AudioSource>().PlayOneShot(harpoon);
            GameObject proj = Instantiate(projectile, this.transform.position, projectile.transform.rotation) as GameObject;
            proj.GetComponent<Projectile>().damage = 100;
            canFire = false;
            counter = coolDown;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        for (int i = 1; i < 6; i++)
        {
            //If I colide with anything enemy
            if (col.CompareTag("Lane" + i))
            {
                //automatically kill what you hit.
                col.GetComponent<EnemyStatScript>().DeltaHealth(-col.GetComponent<EnemyStatScript>().Health);
            }
        }

    }
}
