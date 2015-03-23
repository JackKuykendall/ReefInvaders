using UnityEngine;
using System.Collections;

public class TreasureGet : MonoBehaviour {

    public float counter;
    private Vector3 Up;
    private Vector3 disappear;
	// Use this for initialization

    void Awake()
    {
        AudioManager.Money();
    }
	void Start () {
        disappear = new Vector3(Screen.width / 1.1f, Screen.height/1.05f, 0f);
        disappear = Camera.main.ScreenToWorldPoint(disappear);

        Up = new Vector3(Screen.width / 1.1f, Screen.height/1.0f, transform.position.z);
        Up = Camera.main.ScreenToWorldPoint(Up);
        Up.z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
    {
	if(counter <= 0)
    {
        transform.position = Vector3.Lerp(transform.position, Up, .075f);
    }
    else
    {
        counter -= Time.deltaTime;
    }

        if(transform.position.y >= disappear.y - .1f && transform.position.x >= disappear.x - .1f)
        {
            Destroy(this.gameObject);
        }
	}
}
