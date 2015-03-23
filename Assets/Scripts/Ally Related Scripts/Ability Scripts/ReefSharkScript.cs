using UnityEngine;
using System.Collections;

public class ReefSharkScript : CooldownTimerScripts {

    //boolean that represents whether or not the shark is using its ability
    private bool isActive = false;

    public AudioClip zoom;
	// Use this for initialization
	void Start () 
    {
	}

    public override void Update()
    {

        //if active then speed forward
        if (isActive)
        {
            //target set to far overshoot the edge of the screen to make sure the shark makes it there.
            Destroy(cooldownTimer);
            Vector3 target = new Vector3(Screen.width + 200f, transform.position.y, transform.position.z);

            if (transform.position.x >= Camera.main.ScreenToWorldPoint(target).x)
            {
                //if it gets off the screen, it dies.
                Destroy(this.gameObject);
            }
            //code for actual movement.
            //Used MoveToward function because it functions like Lerp, and it seems more lifelike.
            transform.position = Vector3.MoveTowards(transform.position, target, .30f);
        }
        else
        {
            base.Update();
        }

        
    }
    public void Clicked()
    {
        //this sets the shark to active which will cause his to start moving
        if (canFire)
        {
            //audio usage
            GetComponent<AudioSource>().PlayOneShot(zoom);

            isActive = true;
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
                col.GetComponent<EnemyStatScript>().DeltaHealth(-col.GetComponent<EnemyStatScript>().maxHealth);
            }
        }

    }
}
