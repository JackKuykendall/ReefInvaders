using UnityEngine;
using System.Collections;

public class LevelSelectGUI: MonoBehaviour 
{
	public GUIStyle style;
	public AudioClip click;
	public Texture2D backgroundImage;
	//Int holding the selected Level's index if a level is selected
	int levelIsSelected = 0;
	
	float fontSize;
	
	int _worldInt;
	int _totalWorlds;
	
	float _levelButtonWidth;
	float _levelButtonHeight;
	
	float _column1;
	float _column2;
	float _column3;
	
	float _row1;
	float _row2;
	float _row3;
	
	#region Modal Window Variables
	float _windowPercentage = .8f;
	float _windowWidth;
	float _windowHeight;
	float _windowButtonPercentage = .2f;
	float _windowButtonWidth;
	float _windowButtonHeight;
	float _windowYTartget;
	float _windowYCurrent;
	float _windowYInitial;
	float _lerpPercent = .05f;
	#endregion
	
	// Use this for initialization
	void Start () 
	{
		style.font = Resources.Load<Font>("Fonts/COURIERSTD");
		style.normal.textColor = Color.white;
		
		_worldInt = 1;
		_totalWorlds = 3;
		
		_levelButtonWidth = 0.12f;
		_levelButtonHeight = _levelButtonWidth * 1.76471f;
		
		_column1 = 0.585f;
		_column2 = _column1 + _levelButtonWidth;
		_column3 = _column2 + _levelButtonWidth;
		
		_row1 = .2f;
		_row2 = _row1 + _levelButtonHeight;
		_row3 = _row2 + _levelButtonHeight;
		
		//Model Window Stuff
		_windowWidth = Screen.width*_windowPercentage;
		_windowHeight = Screen.height*_windowPercentage;
		_windowButtonWidth = _windowWidth*_windowButtonPercentage;
		_windowButtonHeight = _windowHeight*_windowButtonPercentage;
		_windowYInitial = -Screen.height;
		_windowYTartget = Screen.height * ((1 - _windowPercentage) * .5f);
		_windowYCurrent = _windowYInitial;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fontSize = Screen.width * 0.03f;
		
		style.fontSize = (int)(fontSize);
		
		//Debug.Log(_row3 + _levelButtonHeight);
		//Debug.Log(.37f + .5f);
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImage);
		if(GUI.Button(new Rect(Screen.width * .025f, Screen.height * .01f, (Screen.width)*0.085f,(Screen.height)*0.15f), "Back"))
		{
            
			Application.LoadLevel("SceneStart");
		}
		
		if(Input.GetKey(KeyCode.Escape))
		{
			GetComponent<AudioSource>().PlayOneShot(click);
			Application.LoadLevel("SceneStart");
		}
		
		GUI.Box(new Rect(Screen.width * 0.425f, Screen.height * -0.01f, Screen.width * 0.15f, Screen.height * 0.15f), "Level Select");
		
		if (GUI.Button(new Rect(Screen.width * 0.33f, Screen.height * 0.2f, Screen.width * 0.085f, Screen.height * 0.15f), "Next"))
		{
			GetComponent<AudioSource>().PlayOneShot(click);
			if (_worldInt + 1 > _totalWorlds)
			{
				_worldInt = 1;
			}
			else
			{
				_worldInt += 1;
			}
		}
		
		if (GUI.Button(new Rect(Screen.width * 0.055f, Screen.height * 0.2f, Screen.width * 0.085f, Screen.height * 0.15f), "Prev"))
		{
			GetComponent<AudioSource>().PlayOneShot(click);
			if (_worldInt - 1 < 1)
			{
				_worldInt = _totalWorlds;
			}
			else
			{
				_worldInt -= 1;
			}
		}
		
		if (_worldInt == 1)
		{
			GUI.Box(new Rect(Screen.width * 0.16f, Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.15f), "World 1");
			GUI.Box(new Rect(Screen.width * 0.055f, Screen.height * 0.37f, Screen.width * 0.36f, Screen.height * 0.465296f), "World 1 Art");
			
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 1"))
			{

				//Application.LoadLevel("W1L1");
				levelIsSelected = 1;
			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 2"))
			{

				//Application.LoadLevel("W1L2");
				levelIsSelected = 2;
			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 3"))
			{

				//Application.LoadLevel("W1L3");
				levelIsSelected = 3;
			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 4"))
			{

				//Application.LoadLevel("W1L4");
				levelIsSelected = 4;
			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 5"))
			{

				levelIsSelected = 5;
			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 6"))
			{

				levelIsSelected = 6;
			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 7"))
			{

				levelIsSelected = 7;
			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 8"))
			{

				levelIsSelected = 8;
			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W1Level 9"))
			{

				levelIsSelected = 9;
			}
		}
		
		if (_worldInt == 2)
		{
			GUI.Box(new Rect(Screen.width * 0.16f, Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.15f), "World 2");
			GUI.Box(new Rect(Screen.width * 0.055f, Screen.height * 0.37f, Screen.width * 0.36f, Screen.height * 0.465296f), "World 2 Art");
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 1"))
			{

				//Application.LoadLevel("SceneGame");
			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 2"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 3"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 4"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 5"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 6"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 7"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 8"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W2Level 9"))
			{

			}
		}
		
		if (_worldInt == 3)
		{
			GUI.Box(new Rect(Screen.width * 0.16f, Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.15f), "World 3");
			GUI.Box(new Rect(Screen.width * 0.055f, Screen.height * 0.37f, Screen.width * 0.36f, Screen.height * 0.465296f), "World 3 Art");
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 1"))
			{

				//Application.LoadLevel("SceneGame");
			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 2"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row1, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 3"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 4"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 5"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row2, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 6"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column1, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 7"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column2, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 8"))
			{

			}
			if (GUI.Button(new Rect(Screen.width * _column3, Screen.height * _row3, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), "W3Level 9"))
			{

			}
		}
		if (levelIsSelected > 0) 
		{
			PlaySelectedLevel(levelIsSelected);
		}
	}
	void PlaySelectedLevel(int levelIndex)
	{
		//Lerps the Y position of the box to get the smooth transition in
		_windowYCurrent = SceneManager.Lerp(_windowYCurrent, _windowYTartget, _lerpPercent);
		//Sets the box itself
		GUI.Box(new Rect(Screen.width * ((1 - _windowPercentage)*.5f), _windowYCurrent,_windowWidth, _windowHeight), "You have selected World 1 Level " + levelIsSelected);
		//First Button to play
		if (GUI.Button(new Rect(_windowWidth * (1f - _windowPercentage), _windowYCurrent + Screen.height * _windowPercentage * .75f,_windowButtonWidth, _windowButtonHeight), "Play This Level!"))
		{
            AudioManager.Click();
            GameObject AM = GameObject.FindGameObjectWithTag("AudioManager");
            Destroy(AM);
            
			Application.LoadLevel("W1L" + levelIsSelected);
		}
		//Second Button to return to level Select;
		if (GUI.Button(new Rect((_windowWidth * (1f - _windowPercentage))+_windowButtonWidth*1.5f, _windowYCurrent + Screen.height * _windowPercentage * .75f, _windowButtonWidth, _windowButtonHeight), "Select Another Level"))
		{
			levelIsSelected = 0;
			_windowYCurrent = _windowYInitial;
		}
	}
}
