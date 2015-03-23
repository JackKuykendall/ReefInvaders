using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public static bool isPaused = true;
	public static bool isInSelection = true;
	public Texture2D[] backgroundImages;
	public bool hasWon;
	public bool doneUnlockingFish;
	public bool hasLost;
	private int reefLifeCounter;
    public GUIStyle net;
    public GUIStyle plank;
	private GameObject backgroundImageObject;
	private GameObject fishToUnlock;
	// Use this for initpialization
	
	#region Modal Window Variables
	float _windowPercentage = .8f;
	float _windowWidth;
	float _windowHeight;
	float _windowButtonPercentage = .2f;
	float _windowButtonWidth;
	float _windowButtonHeight;
	float _windowYTartget;
	float _windowYCurrent;
	float _lerpPercent = .05f;
	#endregion
	
	public enum Scene{Start,Instructions,GameScene,Lose,Win}
	void Start () 
	{
		//Modal Window Stuff
		_windowYTartget = Screen.height * ((1 - _windowPercentage) * .5f);
		_windowYCurrent = -Screen.height;
		_windowWidth = Screen.width*_windowPercentage;
		_windowHeight = Screen.height*_windowPercentage;
		_windowButtonWidth = _windowWidth*_windowButtonPercentage;
		_windowButtonHeight = _windowHeight*_windowButtonPercentage;
		
		reefLifeCounter = backgroundImages.Length;
		backgroundImageObject = GameObject.FindGameObjectWithTag("BackgroundPlane");
		isPaused = true;
		isInSelection = true;
		backgroundImageObject.GetComponent<Renderer>().material.mainTexture = backgroundImages[backgroundImages.Length-1];
		fishToUnlock = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameGUI>().fishToUnlock;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.isPaused) 
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			SceneManager.isPaused = true;
		}
		
	}
	
	void OnGUI()
	{
		//Indicates if the player has lost the level
		if (hasLost)
		{
			isPaused = true;
			float spacer = 1.5f;
			//Lerps the Y position of the box to get the smooth transition in
			_windowYCurrent = Lerp(_windowYCurrent, _windowYTartget, _lerpPercent);
			//Sets the box itself
			GUI.Box(new Rect(Screen.width * ((1 - _windowPercentage)*.5f), _windowYCurrent *.5f - 125f,_windowWidth, _windowHeight * 1.25f), "All your reefs are belong to lionfish!", net);
			//First Button to replay
			if (GUI.Button(new Rect(_windowWidth * (1 - _windowPercentage), _windowYCurrent + Screen.height * _windowPercentage * .75f,_windowButtonWidth, _windowButtonHeight), "Replay This Level",plank))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			//Second Button to return to main menu
			if (GUI.Button(new Rect((_windowWidth * (1 - _windowPercentage))+_windowButtonWidth*spacer, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Return To Home", plank))
			{
				Application.LoadLevel("SceneStart");
			}
			//Third button to go to level select
			if (GUI.Button(new Rect((_windowWidth * (1 - _windowPercentage))+_windowButtonWidth*spacer*2, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Level Select", plank))
			{
				Application.LoadLevel("SceneLevelSelect");
			}
		}
		//Indicated if the player has won the level
		if (hasWon)
		{
			isPaused = true;
			float spacer = 1.5f;
			if (fishToUnlock == null) {
				doneUnlockingFish = true;
			}
			if (doneUnlockingFish) 
			{
				
				
				//Lerps the Y position of the box to get the smooth transition in
				_windowYCurrent = Lerp(_windowYCurrent, _windowYTartget, _lerpPercent);
				//Sets the box itself
				GUI.Box(new Rect(Screen.width * ((1 - _windowPercentage)*.5f), _windowYCurrent *.5f - 125f,_windowWidth, _windowHeight * 1.25f), "You Won!", net);
				//First Button to replay
				if (GUI.Button(new Rect(_windowWidth * (1 - _windowPercentage), _windowYCurrent + Screen.height * _windowPercentage * .75f,_windowButtonWidth, _windowButtonHeight), "Play Next Level", plank))
				{
					Application.LoadLevel(Application.loadedLevel+1);
				}
				//Second Button to return to main menu
				if (GUI.Button(new Rect((_windowWidth * (1 - _windowPercentage))+_windowButtonWidth*spacer, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Return To Home", plank))
				{
					Application.LoadLevel("SceneStart");
				}
				//Third button to go to level select
				if (GUI.Button(new Rect((_windowWidth * (1 - _windowPercentage))+_windowButtonWidth*spacer*2, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Level Select", plank))
				{
					Application.LoadLevel("SceneLevelSelect");
				}
			}
			else
			{
				//Lerps the Y position of the box to get the smooth transition in
				_windowYCurrent = Lerp(_windowYCurrent, _windowYTartget, _lerpPercent);
				//Sets the box itself
				GUI.Box(new Rect(Screen.width * ((1 - _windowPercentage)*.5f), _windowYCurrent*.5f - 125f,_windowWidth, _windowHeight *1.25f), "You Unlocked " + fishToUnlock.name + "!", net);
				//First Button to Learn More
				if (GUI.Button(new Rect(_windowWidth * (1 - _windowPercentage), _windowYCurrent + Screen.height * _windowPercentage * .75f,_windowButtonWidth, _windowButtonHeight), "Learn About this Reef Defender!", plank))
				{
					Application.LoadLevel("SceneBios");
				}
				//Fish Texture
				GUI.DrawTexture(new Rect(Screen.width*.5f - _windowButtonWidth*.5f, _windowYCurrent + _windowButtonWidth*.5f,_windowButtonWidth,_windowButtonWidth),fishToUnlock.GetComponent<UnitStatScript>().tex);

				//Second button to go to continue
				if (GUI.Button(new Rect((_windowWidth * (1 - _windowPercentage))+_windowButtonWidth*spacer*2, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Continue", plank))
				{
					doneUnlockingFish = true;
				}
			}
			
		}
	}
	
	public void ChangeScene(Scene scene)
	{
		if (scene == Scene.Start) 
		{
			Application.LoadLevel("SceneStart");
		}
		if (scene == Scene.Instructions) 
		{
			Application.LoadLevel("SceneInstructions");
		}
		if (scene == Scene.GameScene) 
		{
			Application.LoadLevel("SceneGame");
		}
	}
	public void DamageReef()
	{
		if (reefLifeCounter <= 1) 
		{
			hasLost = true;
			return;
		}
		if (reefLifeCounter > 1) 
		{
			reefLifeCounter--;
		}
		backgroundImageObject.GetComponent<Renderer>().material.mainTexture = backgroundImages[reefLifeCounter-1];
	}
	
	//Basic Lerp Function
	public static float Lerp(float currentPos, float targetPos, float percentLerp)
	{
		return ((targetPos - currentPos) * percentLerp) + currentPos;
	}
}
