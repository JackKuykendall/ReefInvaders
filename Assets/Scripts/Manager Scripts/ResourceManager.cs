using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    public GameObject treasureGet;
	public int resource = 200;
    [HideInInspector]
    public Vector3 moneySpawn;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.isPaused) 
		{
			return;
		}
	}
	public int Resource
	{
		get
		{
			return resource;
		}
	}
	public void DeltaResource(int delta)
	{
        AudioManager.Click();
		resource += delta;
	}

    public void DeltaResourceTG(int delta, Vector3 position)
    {
        moneySpawn = position;
        AudioManager.Click();
        Instantiate(treasureGet, moneySpawn, Quaternion.identity);
        resource += delta;
    }
	public bool hasResources(int cost)
	{
		if (cost <= resource)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
}
