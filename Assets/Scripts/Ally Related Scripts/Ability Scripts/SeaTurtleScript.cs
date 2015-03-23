using UnityEngine;
using System.Collections;

public class SeaTurtleScript : CooldownTimerScripts
{

    [HideInInspector]
    private float isInvul = -1f;
    private int previousHealth;
    private UnitStatScript script;
    // Use this for initialization
    void Start()
    {
        script = this.gameObject.GetComponent<UnitStatScript>();
        canFire = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isInvul >= 0)
        {

            script.color.r = 0.2f;
            script.color.g = 0.4f;
            script.color.b = 1f;
            script.GetComponent<Renderer>().material.color = script.color;
            if (this.gameObject.GetComponent<UnitStatScript>().GetHealth() < previousHealth)
            {

                int tempHP = previousHealth - this.gameObject.GetComponent<UnitStatScript>().GetHealth();
                this.gameObject.GetComponent<UnitStatScript>().DeltaHealth(tempHP);

            }
        }
        else
        {
            script.color.r = 1f;
            script.color.g = 1f;
            script.color.b = 1f;
            script.isActive = false;
        }

        
        isInvul -= Time.deltaTime;
        base.Update();

    }
    public void Clicked()
    {
        if (canFire)
        {
            script.isActive = true;
            AudioManager.PlayAbilitySound(1, 1, 1);
            canFire = false;
            isInvul = 3;
            counter = coolDown;
            previousHealth = this.gameObject.GetComponent<UnitStatScript>().GetHealth();
        }
    }

}
