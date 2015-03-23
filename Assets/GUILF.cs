using UnityEngine;
using System.Collections;

public class GUILF : MonoBehaviour {

	// Use this for initialization
    public Vector2 direction;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        GetComponent<Rigidbody2D>().AddForce(direction * 1f, ForceMode2D.Impulse);
	}
}
