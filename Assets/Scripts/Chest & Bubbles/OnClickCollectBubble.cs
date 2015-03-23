using UnityEngine;
using System.Collections;

public class OnClickCollectBubble : MonoBehaviour {
	public int reward;
	private ResourceManager resourceManager;
    private Vector3 velocity; 

	// Use this for initialization
	void Start () {
		resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        velocity = this.gameObject.GetComponent<Rigidbody>().velocity;
        if (!SceneManager.isPaused)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().velocity = velocity;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
	
	}
	public void Clicked()
	{
		resourceManager.DeltaResourceTG(reward, transform.position);
		Destroy(this.gameObject);
	}
}
