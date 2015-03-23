using UnityEngine;
using System.Collections;

public class StartMenuGUI : MonoBehaviour
{
	public GUIStyle guiStyle;
    public AudioClip click;
	public float buttonWidthDivider = 3.33f;
	public float buttonHeightDivider = 10f;
	public float buttonOneWidthOffset;
	public float buttonOneHeightOffset;
	public float buttonTwoWidthOffset;
	public float buttonTwoHeightOffset;
	public float buttonThreeWidthOffset;
	public float buttonThreeHeightOffset;
	public Texture2D backgroundImage;

	private float buttonWidth;
	private float buttonHeight;
	private float halfScreenWidth = Screen.width * .5f;
	private float halfScreenHeight = Screen.height * .5f;
	private Rect position;
	//private Rect positionOne;
	//private Rect positionTwo;
	//private Rect positionThree;
	private SceneManager sceneManger;
	
	// Use this for initialization
	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//sceneManger = GameObject.Find("SceneManager").GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		buttonWidth = Screen.width / buttonWidthDivider;
		buttonHeight = Screen.height / buttonHeightDivider;
		halfScreenWidth = Screen.width * .5f;
		halfScreenHeight = Screen.height * .5f;
		//positionOne = new Rect(position.x + buttonOneWidthOffset, position.y + buttonOneHeightOffset, position.width, position.height);
		//positionTwo = new Rect(position.x + buttonTwoWidthOffset, position.y + buttonTwoHeightOffset, position.width, position.height);
		//positionThree = new Rect(position.x + buttonThreeWidthOffset, position.y + buttonThreeHeightOffset, position.width, position.height);
	}
	public void OnGUI()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundImage);
		
		position = new Rect(halfScreenWidth - buttonWidth * .5f, halfScreenHeight - buttonHeight * .5f, buttonWidth, buttonHeight);
		
		if (GUI.Button(new Rect(position.x, position.y, position.width, position.height), "Start",guiStyle))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneLevelSelect");
		}
		if (GUI.Button(new Rect(position.x, position.y + position.height*2, position.width, position.height), "Instructions",guiStyle))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneInstructions");
		}
		if (GUI.Button(new Rect(position.x, position.y + position.height*4, position.width, position.height), "Quit",guiStyle))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.Quit();
		}
		
		if (GUI.Button(new Rect(Screen.width * .65f + Screen.width/10, position.y +  position.height*4, Screen.width * .25f, position.height), "Get Involved",guiStyle))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneDonate");
		}
		if (GUI.Button(new Rect(Screen.width * .1f - Screen.width/10, position.y +  position.height*4, Screen.width * .25f, position.height), "Get Fishy",guiStyle))
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneBios");
		}
	}
}
