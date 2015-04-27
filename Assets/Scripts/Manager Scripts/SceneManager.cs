using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public Canvas win;
    public Canvas lose;
    private bool hasPlayed = false;
	public static bool isPaused = true;
	public static bool isInSelection = true;
	public Texture2D[] backgroundImages;
    public bool hasWon;
    public bool hasLost;
	public static bool hasWonStat;
	public bool doneUnlockingFish;
	public static bool hasLostStat;
	private int reefLifeCounter;
    public GUIStyle net;
    public GUIStyle plank;
	private GameObject backgroundImageObject;
	private GameObject fishToUnlock;
    private float transitionTime = 2f;
	// Use this for initialization
	
	public enum Scene{Start,Instructions,GameScene,Lose,Win}
	void Start () 
	{	
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
            GameGUI.shouldShowPauseScreen = true;
		}
        hasWonStat = hasWon;
        hasLostStat = hasLost;
		
	}
	
	void OnGUI()
	{
		//Indicates if the player has lost the level
		if (hasLost)
		{
            if (transitionTime > 0)
            {
                transitionTime -= Time.deltaTime;
                return;
            }
			isPaused = true;
            if(!hasPlayed)
            {
                lose.GetComponent<Animation>().Play();
                hasPlayed = true;
            }
            

		}
		//Indicated if the player has won the level
		if (hasWon)
		{
            if (transitionTime > 0)
            {
                transitionTime -= Time.deltaTime;
                return;
            }
            
			isPaused = true;
			if (fishToUnlock == null) {
				doneUnlockingFish = true;
			}
			if(!hasPlayed)
			{
				
                win.GetComponent<Animation>().Play();
                hasPlayed = true;
			}
			
		}
	}
	
	public void ReplayLevel()
	{
        doneUnlockingFish = true;
        AudioManager.Click();
        Application.LoadLevel(Application.loadedLevel);
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
    public void KillReef()
    {
        hasLost = true;
        backgroundImageObject.GetComponent<Renderer>().material.mainTexture = backgroundImages[0];

    }
	
	//Basic Lerp Function
	public static float Lerp(float currentPos, float targetPos, float percentLerp)
	{
		return ((targetPos - currentPos) * percentLerp) + currentPos;
	}
}
