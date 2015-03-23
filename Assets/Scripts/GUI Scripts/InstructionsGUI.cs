using UnityEngine;
using System.Collections;

public class InstructionsGUI: MonoBehaviour {

	public Texture2D[] instructionsScreen;
	int instructionsScreenIndex = 0;

    public AudioClip click;
	//public float buttonThreeWidthOffset;
	//public float buttonThreeHeightOffset;
	private float buttonWidth;
	private float buttonHeight;
	private float halfScreenWidth = Screen.width/2;
	private float halfScreenHeight = Screen.height/2;
	private Rect position;
	private Rect positionOne;
	private Rect positionTwo;
	//private Rect positionThree;
	
	// Use this for initialization
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () 
	{
		buttonWidth = Screen.width/16;
		buttonHeight = Screen.height/6;
		halfScreenWidth = Screen.width/2;
		halfScreenHeight = Screen.height/2;
		position = new Rect(0,0,Screen.width,Screen.height);
		positionOne = new Rect(0,halfScreenHeight - buttonHeight/2,buttonWidth,buttonHeight);
		positionTwo = new Rect(Screen.width - buttonWidth,halfScreenHeight - buttonHeight/2,buttonWidth,buttonHeight);
		//positionThree = new Rect(position.x + buttonThreeWidthOffset,position.y + buttonThreeHeightOffset, buttonWidth,buttonHeight);

	}
	public void OnGUI()
	{
		GUI.Box(position,instructionsScreen[instructionsScreenIndex]);
		if (GUI.Button(positionOne,"<--")) 
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			if (instructionsScreenIndex > 0) 
			{
				instructionsScreenIndex--;
			}
		}
		if (GUI.Button(positionTwo,"-->")) 
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			if (instructionsScreenIndex < instructionsScreen.Length-1) 
			{
				instructionsScreenIndex++;
			}
		}

		//FIX POSITION!!! IT IS NOT CORRECT
		if (GUI.Button(new Rect(halfScreenWidth,Screen.height-Screen.height/12,buttonWidth*3,Screen.height/12),"Main Menu")) 
		{
			GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneStart");
		}

	}
}
