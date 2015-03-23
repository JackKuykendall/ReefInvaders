using UnityEngine;
using System.Collections;

public class WinGUI : MonoBehaviour {

    public AudioClip click;
	public float buttonWidthDivider = 3.33f;
	public float buttonHeightDivider = 5f;
	public float buttonOneWidthOffset;
	public float buttonOneHeightOffset;
	public float buttonTwoWidthOffset;
	public float buttonTwoHeightOffset;
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
	private SceneManager sceneManger;
	
	// Use this for initialization
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
		sceneManger = GameObject.Find("SceneManager").GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		buttonWidth = Screen.width/buttonWidthDivider;
		buttonHeight = Screen.height/buttonHeightDivider;
		halfScreenWidth = Screen.width/2;
		halfScreenHeight = Screen.height/2;
		position = new Rect(halfScreenWidth - buttonWidth/2,halfScreenHeight - buttonHeight/2,buttonWidth,buttonHeight);
		positionOne = new Rect(position.x + buttonOneWidthOffset,position.y + buttonOneHeightOffset, buttonWidth,buttonHeight);
		positionTwo = new Rect(position.x + buttonTwoWidthOffset,position.y + buttonTwoHeightOffset, buttonWidth,buttonHeight);
		//positionThree = new Rect(position.x + buttonThreeWidthOffset,position.y + buttonThreeHeightOffset, buttonWidth,buttonHeight);
	}
	public void OnGUI()
	{
		if (GUI.Button(positionOne,"Replay")) 
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			sceneManger.ChangeScene(SceneManager.Scene.GameScene);
		}
		if (GUI.Button(positionTwo,"Main Menu")) 
		{
            GetComponent<AudioSource>().PlayOneShot(click);
			sceneManger.ChangeScene(SceneManager.Scene.Start);
		}
		
	}
}
