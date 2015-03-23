using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishSelectionGUI : MonoBehaviour {

    public GUIStyle Net;
    public GUIStyle Plank;
    public GUIStyle myStyle;
    public Texture2D squareBubble;
    public Texture2D testTexture;
    private Rect rectangle;
    private Rect selectionBox;
    private Rect selected;
    private Rect confirm;

	public int alliesToSelect;
	public GameObject[] fishToBeChosen;
	public Material lockedMaterial;
	public GameGUI gameGUI;
    public List<Rect> choices = new List<Rect>();

	void Start () {

		gameGUI = this.gameObject.GetComponent<GameGUI>();
        rectangle = new Rect(Screen.width / 8, -1f, Screen.width * .75f, Screen.height * .9f);
        selected = new Rect(rectangle.x + rectangle.width * .3f, Screen.height / 8 + (Screen.height * .75f) /20, rectangle.width* .4f, rectangle.height * .1f);
        selectionBox = new Rect(selected.x + (rectangle.width * 1.8f) / 10, Screen.height / 8 + (Screen.height * .75f) / 20, rectangle.width / 2 - rectangle.width / 20, rectangle.height - rectangle.height / 10);
        confirm = new Rect(Screen.width / 2 - Screen.height / 8 + Screen.height/35, Screen.height - Screen.height / 3.5f, Screen.width / 8, Screen.height / 10);

        Plank.fontSize = Screen.width / 40;

        choices.Add(new Rect((rectangle.x + rectangle.width / 10) + selected.width / 20, selected.y + selected.height * 2f, (rectangle.width - rectangle.width / 5) / 7, (rectangle.width - rectangle.width / 5) / 7));
        for(int i = 1; i<= fishToBeChosen.Length - 1; i++)
        {
            if (choices.Count % 6 != 0)
            {
                choices.Add(new Rect(choices[i - 1].x + choices[i - 1].width + (rectangle.width * .2f) * .075f, choices[i - 1].y, choices[i - 1].width, choices[i - 1].height));
            }
            else
            {
                choices.Add(new Rect(choices[0].x, choices[i - 1].y + choices[i - 1].height + (rectangle.width * .2f)*.075f, choices[i - 1].width, choices[i - 1].height));
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        
        if (SceneManager.isInSelection)
        {
            GUI.Box(rectangle, "", Net);
            GUI.Box(selected, "Choose Your Allies!", Plank);

            for (int i = 0; i < choices.Count; i++)
            {
				if (fishToBeChosen[i] != null) 
				{
                    UnitStatScript fish = fishToBeChosen[i].GetComponent<UnitStatScript>();
                    myStyle.fontSize = (int)choices[i].height / 7;
					if(GUI.Button(choices[i], new GUIContent("$" + fish.cost.ToString(),fish.tex), myStyle))
	                {
                        
						int numberOfFishSelected = 0;
						foreach (GameObject obj in gameGUI.FishSelected) 
						{
                            AudioManager.Click();
							if (obj != null) 
							{
								++numberOfFishSelected;
							}

						}
						if (!gameGUI.FishSelected.Contains(fishToBeChosen[i])) 
						{
							if (numberOfFishSelected < 4) 
							{
								gameGUI.FishSelected.Add(fishToBeChosen[i]);
                                AudioManager.Click();
							}
						}
						else 
						{
							gameGUI.FishSelected.Remove(fishToBeChosen[i]);
                            AudioManager.Click();
						}
	                    break;
	                }
				}
				else 
				{
					if(GUI.Button(choices[i], lockedMaterial.GetTexture(0), myStyle))
					{
                        AudioManager.Click();
						break;
					}
				}
            }

            
            if (GUI.Button(confirm, "Confirm", Plank))
            {
                AudioManager.Click();
                if (SceneManager.isInSelection)
                {
					if (gameGUI.FishSelected.Count == alliesToSelect) 
					{
	                    SceneManager.isPaused = false;
	                    SceneManager.isInSelection = false;
						this.gameObject.GetComponent<GameGUI>().levelCountdown = 5f;
					}
                }

            }
        }
      
    }
}
