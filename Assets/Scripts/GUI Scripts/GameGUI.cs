using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGUI : MonoBehaviour {
	public bool shouldShowPauseScreen = false;
	public GameObject fishToUnlock;

	public float buttonWidthDivider = 3.33f;
	public float buttonHeightDivider = 5f;
	public string currencyString = "Funding: ";
	public float currencyXOffset;
	public float currencyYOffset;
	public float currencyWidth = 50f;
	public float currencyHeight = 10f;
    public Texture mainMenu;
    public Texture levelSelect;
    public Texture restart;
    public Texture pause;
    public Texture play;
    public GUIStyle myStyle;
	public GUIStyle boat;
    public GUIStyle treasure;
    public GUIStyle chain;
	public GUIStyle unPauseGUI;
	public GUIStyle countdown;

	//[HideInInspector]
	public List<GameObject> FishSelected = new List<GameObject>();

	public float levelCountdown = 0f;
	float levelCountdownSize;

	//Main menu button variables
	private float mmButtonWidth;
	private float mmButtonHeight;
	private Rect mmButtonPosition;
	
	//Restart menu buttons variables
	private float rButtonWidth;
	private float rButtonHeight;
	private Rect rButtonPosition;
	
	//Level Select button variables
	private float lsButtonWidth;
	private float lsButtonHeight;
	private Rect lsButtonPosition;

    private GUIManager guiManager;
	private ResourceManager resourceManager;
	private Rect currencyStringRect;
	private float halfScreenWidth = Screen.width/2;
	private float halfScreenHeight = Screen.height/2;
	private SceneManager sceneManager;
	private BuildManager buildManager;
	private int fakeIndexer;
	// Use this for initialization
	void Start () 
	{
		levelCountdownSize = Screen.width/10;
		buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();
		resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
		currencyStringRect = new Rect(halfScreenWidth + currencyXOffset,currencyYOffset,currencyWidth,currencyHeight);
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
		countdown.fontSize = Screen.width/10;
	}
	
	// Update is called once per frame
	void Update () 
	{
        treasure.fontSize = Screen.height / 23;
        myStyle.fontSize = (Screen.height / 10) / 4;     
	}


	void OnGUI()
	{
		//Counts down the time before the level starts!
		if ((int)levelCountdown > 0) 
		{
			GUI.Box(new Rect(Screen.width/2 - levelCountdownSize/2,Screen.height/2 - levelCountdownSize/2,levelCountdownSize,levelCountdownSize),"" + (int)levelCountdown,countdown);
			//OnGUI is called twice a frame, Who knew!?
			levelCountdown -= Time.deltaTime/2;
		}
        
		//Main Menu Button Sizing and position variables
		mmButtonWidth = Screen.width/buttonWidthDivider;
		mmButtonHeight = Screen.height/buttonHeightDivider;
		mmButtonPosition = new Rect(Screen.width*.5f - mmButtonWidth*.5f, Screen.height*.5f - mmButtonHeight*.5f,mmButtonWidth,mmButtonHeight);
		
		//Restart Button Sizing and position variables
		rButtonWidth = Screen.width / buttonWidthDivider;
		rButtonHeight = Screen.height / buttonHeightDivider;
		rButtonPosition = new Rect(Screen.width / 3 - rButtonWidth*.5f, Screen.height *.5f - rButtonHeight *.5f, rButtonWidth, rButtonHeight);
		
		//Level Select Button Sizing and position variables
		lsButtonWidth = Screen.width / buttonWidthDivider;
		lsButtonHeight = Screen.height / buttonHeightDivider;
		lsButtonPosition = new Rect(Screen.width / (float)1.5 - rButtonWidth *.5f, Screen.height *.5f - rButtonHeight *.5f, rButtonWidth, rButtonHeight);
		
		//Debug.Log(currencyStringRect);


		//Side Bar GUI

		fakeIndexer = 0;
		foreach (GameObject fish in FishSelected) 
		{
			if (fish != null) 
			{
				//BE VERY CAREFUL HERE! CHANGING THE FISH SELECTED WILL IN FACT CHANGE THE PREFAB, THE FISH SELECTED PASSES AN INSTANCE OF THE PREFAB TO ALLOW ACCESS FOR ALL ITS INFORMATION
                Rect rectangle = new Rect(Screen.height / 50, Screen.width / 15 + fakeIndexer * (Screen.width / 10) + fakeIndexer * (Screen.width / 50), Screen.width / 10, Screen.width / 10);
                Texture texture = fish.GetComponent<UnitStatScript>().tex;
                if (fish.GetComponent<UnitStatScript>().cost > resourceManager.resource)
                {
                    myStyle.normal.textColor = Color.red;
                }
                else
                {
                    myStyle.normal.textColor = Color.green;
                }

                if (GUI.Button(rectangle, new GUIContent("$" + fish.GetComponent<UnitStatScript>().cost.ToString(), texture), myStyle)) 
				{
					if (SceneManager.isInSelection) 
					{

					}
					else 
					{
						UnitStatScript scriptRef = fish.GetComponent<UnitStatScript>();
						if (resourceManager.resource >= scriptRef.cost) 
						{
							buildManager.SelectOrDeselectObjectToBuild(fish,scriptRef.cost);
                            AudioManager.Click();
						}
					}
				}
			}
			fakeIndexer++;
		}

		if (!SceneManager.isInSelection) 
		{
            Rect rectangle = new Rect(Screen.width / 30, Screen.height / 150f, Screen.width / 10f, Screen.height / 10f);
            if (!SceneManager.isPaused)
            {
                if (GUI.Button(rectangle, pause, unPauseGUI))
                {
                    AudioManager.Click();

					shouldShowPauseScreen = true;
                    SceneManager.isPaused = true;

                }
            }
			if (SceneManager.isPaused) 
			{
                if(SceneManager.hasLostStat || SceneManager.hasWonStat)
                {
                    shouldShowPauseScreen = false;
                }
				if (shouldShowPauseScreen) 
				{	
	                if (GUI.Button(rectangle, play, unPauseGUI))
	                {
	                    SceneManager.isPaused = false;
                        shouldShowPauseScreen = false;
	                }
					//creates Main Menu button
					if (GUI.Button(mmButtonPosition,mainMenu,unPauseGUI)) 
					{
                        AudioManager.Click();
                        guiManager.ChangeToMenu("SceneStart");
					}
					
					//creates Level Select button
					if (GUI.Button(lsButtonPosition,levelSelect,unPauseGUI)) 
					{
                        AudioManager.Click();
                        guiManager.ChangeToMenu("SceneLevelSelect");
					}
					
					//Creates Restart button
					if (GUI.Button(rButtonPosition,restart,unPauseGUI)) 
					{
                        AudioManager.Click();
						Application.LoadLevel(Application.loadedLevel);
					}
					if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "", unPauseGUI))
					{
                        AudioManager.Click();
						SceneManager.isPaused = false;
						shouldShowPauseScreen = false;
					}
				}

               
				
			}
            else
            {
                if (!UnitManager.shouldSell)
                {
                    treasure.normal.textColor = Color.green;
                    if (GUI.Button(new Rect(Screen.width / 1.5f, -Screen.height / 40, Screen.width / 8, Screen.height / 8), "Sell", treasure))
                    {

                        AudioManager.Click();
                        UnitManager.shouldSell = true;

                    }
                }
                else
                {
                    treasure.normal.textColor = Color.red;
                    if (GUI.Button(new Rect(Screen.width / 1.5f, -Screen.height / 20, Screen.width / 8, Screen.height / 8), "Cancel", treasure))
                    {

                        AudioManager.Click();
                        UnitManager.shouldSell = false;

                    }
                }
            }

            
            
            
            }
       
            
            
		
		//Top Line GUI
        treasure.normal.textColor = Color.yellow;
        GUI.Box(new Rect(Screen.width / 1.165f, -Screen.height / 40, Screen.width / 8, Screen.height / 8), currencyString + resourceManager.resource, treasure);      
	}
}
