using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public bool shouldShowHints = true;
    public float bubbleIndex;
    public float fishIndex;
    public float lionFishIndex;
    public bool showLionFishHint;
    public bool showBubbleHint;
    public bool showAngelFishHint;
    public Text[] hideHints = new Text[3];
    public Canvas[] tutorials = new Canvas[3];
    public Texture2D bubbleTexture;
    public Texture2D angelFishTexture;
    public Texture2D fishPlankTexture;
    public Texture2D lionfishTexture;
    public GUIStyle plankGUI;
    public GUIStyle textGUI;
    private string bubbleText;

    #region Modal Window Variables
    float _windowYTartget;
    float _windowYCurrent;
    #endregion
    // Use this for initialization
    void Start()
    {
        //Modal Window Stuff

        tutorials[0].GetComponent<Animation>().wrapMode = WrapMode.Once;
        textGUI.fontSize = Screen.width / 30;
        plankGUI.fontSize = Screen.width / 30;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Bubble()
    {
        if (bubbleIndex > 0 || shouldShowHints == false)
        {
            return;
        }
        showBubbleHint = true;
        bubbleIndex++;
    }
    public void Fish()
    {
        if (fishIndex > 0 || shouldShowHints == false)
        {
            return;
        }
        showAngelFishHint = true;
        fishIndex++;
    }
    public void LionFish()
    {
        if (lionFishIndex > 0 || shouldShowHints == false)
        {
            return;
        }
        showLionFishHint = true;
        lionFishIndex++;
    }
    public void OnGUI()
    {
        if (showBubbleHint)
        {
            SceneManager.isPaused = true;
            tutorials[1].GetComponent<Animation>().Play();
            
            showBubbleHint = false;
        }
        if (showAngelFishHint)
        {
            SceneManager.isPaused = true;
            tutorials[0].GetComponent<Animation>().Play();
            showAngelFishHint = false;

        }
        if (showLionFishHint)
        {
            SceneManager.isPaused = true;
            tutorials[2].GetComponent<Animation>().Play();
            showLionFishHint = false;
        }

    }

    public void HideHints(int index)
    {
        AudioManager.Click();
        if(!shouldShowHints)
        {
            shouldShowHints = true;
            hideHints[index].text = "Hide Hints";
        }
        else
        {
            shouldShowHints = false;
            hideHints[index].text = "Show Hints";
        }

    }

    public void Thanks(int index)
    {
        AudioManager.Click();
        tutorials[index].GetComponent<Animation>().PlayQueued("TutorialDone");
        SceneManager.isPaused = false;
    }
}
