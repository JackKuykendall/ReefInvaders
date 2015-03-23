using UnityEngine;
using System.Collections;

public class ProjectileSlow : MonoBehaviour {
	public float slowToPercentageMoveSpeed = .7f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		for (int i = 1; i < 6; i++) 
		{
			//If I colide with anything enemy
			if (col.CompareTag("Lane" + i)) 
			{
				EnemyStatScript scriptRef = col.GetComponent<EnemyStatScript>();
				scriptRef.isSlowed = true;
				scriptRef.SlowDuration = 2f;
				scriptRef.slowPercentage = slowToPercentageMoveSpeed;
			}
		}
	}
}
