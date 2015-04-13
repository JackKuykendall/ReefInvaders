using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldNameDisplayer : MonoBehaviour {
    Text worldText;
	// Use this for initialization
	void Start () {
        worldText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        worldText.text = "World " + GUIManager.worldIndex;
	
	}
}
