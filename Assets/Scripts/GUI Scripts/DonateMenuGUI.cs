using UnityEngine;
using System.Collections;

public class DonateMenuGUI : MonoBehaviour 
{
    public AudioClip click;
	public GUIStyle style;
	
	float fontSize;
	
	
	// Use this for initialization
	void Start () 
	{
		style.font = Resources.Load<Font>("Fonts/COURIERSTD");
		style.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fontSize = Screen.width * 0.03f;
		
		style.fontSize = (int)(fontSize);
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width * .025f, Screen.height * .01f, (Screen.width)*0.085f,(Screen.height)*0.15f), "Back"))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneStart");
		}
		
		if(Input.GetKey(KeyCode.Escape))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneStart");
		}
		
		GUI.Box(new Rect(Screen.width * 0.425f, Screen.height * -0.01f, Screen.width * 0.15f, Screen.height * 0.15f), "Get Involved");
		
		GUI.Box(new Rect(Screen.width * 0.055f, Screen.height * 0.25f, Screen.width * 0.36f, Screen.height * 0.7f), "Donation Pitch");
		
		if (GUI.Button(new Rect(Screen.width * .665f, Screen.height * .4f, (Screen.width) * 0.2f, (Screen.height) * 0.2f), "Donate"))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.OpenURL("https://www.reef.org/contribute");
			
		}
	}
}
