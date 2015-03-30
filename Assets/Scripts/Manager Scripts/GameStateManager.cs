using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour 
{
	public enum GameState{Game,Win,Loss};
	public GameState gameState;
	private SceneManager sceneManager;
	// Use this for initialization
	void Start () 
	{
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		/*if (gameState == GameState.Loss) 
		{
			sceneManager.ChangeScene(SceneManager.Scene.Lose);
		}
		if (gameState == GameState.Win) 
		{
			sceneManager.ChangeScene(SceneManager.Scene.Win);
		}*/
	
	}
}
