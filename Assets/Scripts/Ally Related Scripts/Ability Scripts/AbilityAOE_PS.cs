using UnityEngine;
using System.Collections;

public class AbilityAOE_PS : MonoBehaviour {
	[HideInInspector]
    public int damage;
    public float counter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
    
	if(counter<=0)
    {
        Destroy(this.gameObject);
    }
    else
        counter -= Time.deltaTime;
	}

    void OnTriggerEnter(Collider col)
    {
		if (!col.CompareTag("PlayerUnit")) 
		{
			if (col.GetComponent<EnemyStatScript>() != null) 
			{
				col.GetComponent<EnemyStatScript>().DeltaHealth(-damage);
			}
		}
		/*
        for (int i = 1; i < 6; i++)
        {      
            //If I colide with anything enemy
            if (col.CompareTag("Lane" + i))
            {
                col.GetComponent<EnemyStatScript>().DeltaHealth(-damage);
                break;

            }
        }
        */

    }
}
