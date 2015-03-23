using UnityEngine;
using System.Collections;

public class BioSceneGUI : MonoBehaviour
{
    public GUIStyle style;
    public GUIStyle textOnly;
    public GUIStyle plank;
    public GUIStyle buttonPlank;
    private GameObject[] activeList; //This is the array of fish currently being displayed/drawn. It makes it so we don't have to copy the same block of code for enemies.
    public GameObject[] allies;
    public GameObject[] enemies;
    private float[] _column = new float[3];
    private float _row;
    public Texture lockTex;
    public Texture2D backgroundImage;
    public Texture2D plankImage;
    private Texture tex;
    

    float fontSize;

    //These 2 floats are used to help make tiling the bio tiles in a grid formation easier.
    float _levelButtonWidth;
    float _levelButtonHeight;

    int _shownBio = 0;

    bool isAllies = true; //Bool for whether or not allies is the array being displayed/drawn.

    // Use this for initialization
    void Start()
    {
        _levelButtonWidth = 0.12f;
        _levelButtonHeight = _levelButtonWidth * 1.4f;

        _column[0] = 0.055f;
        _column[1] = _column[0] + _levelButtonWidth;
        _column[2] = _column[1] + _levelButtonWidth;

        activeList = allies;
    }

    // Update is called once per frame
    void Update()
    {
        textOnly.fontSize = (int)(Screen.width * .36f / 22);
        fontSize = Screen.width * 0.03f;

        style.fontSize = (int)(fontSize);
        buttonPlank.fontSize = (int)(fontSize);

        buttonPlank.normal.textColor = Color.yellow;
    }

    void OnGUI()
    {
        _row = .25f;

        #region "Ever Present GUI"
        //This region is for the GUI that is present for both allies and enemies.

        //Background Image
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundImage);

        //Draw the large plank block to hold all the info for the selected fish. This is the Info Box.
        GUI.DrawTexture(new Rect(Screen.width * 0.5f, -Screen.height * 0.125f, Screen.width * 0.5f, Screen.height + Screen.height * 0.25f), plankImage);

        //Title plank that reads "Bios"
        GUI.Box(new Rect(Screen.width * .165f, Screen.height * 0.02f, Screen.width * 0.15f, Screen.height * 0.125f), "Bios", buttonPlank);

        //Back Button
        style.fontSize = (int)(fontSize * .65f);
        if (GUI.Button(new Rect(Screen.width * .015f, Screen.height * .02f, (Screen.width) * 0.085f, (Screen.height) * 0.075f), "Back", style) || Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("SceneStart");
        }
        style.fontSize = (int)(fontSize);

        //Allies Button
        buttonPlank.normal.textColor = Color.green;
        if (GUI.Button(new Rect(Screen.width * .115f, Screen.height * .175f, Screen.width * _levelButtonWidth, Screen.height * .068f), "Allies", buttonPlank))
        {
            isAllies = true;
            _shownBio = 0;
            activeList = allies;
        }

        //Enemies Button
        buttonPlank.normal.textColor = Color.red;
        if (GUI.Button(new Rect(Screen.width * .235f, Screen.height * .175f, Screen.width * _levelButtonWidth, Screen.height * .068f), "Enemies", buttonPlank))
        {
            isAllies = false;
            _shownBio = 0;
            activeList = enemies;
        }

        //Lesson Plan Button
        buttonPlank.normal.textColor = Color.yellow;
        if (GUI.Button(new Rect(Screen.width * .62f, Screen.height * .01f, (Screen.width) * 0.3f, (Screen.height) * 0.15f), "Go To Lesson Plans", buttonPlank))
        {
            //Link to lesson plan website.
        }
        #endregion

        #region "Fish Tiles"
        //This region is for the GUI code for both the ally and enemy fish tiles.

        //Test if the button clicked was null.
        if (activeList[_shownBio] != null)
        {
            //Draw the selected fish's information in the Info Box.
            textOnly.alignment = TextAnchor.UpperCenter;

            if (isAllies)
            {
                //Draw the ally's button texture.
                GUI.DrawTexture(new Rect(Screen.width * 0.58f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * .35f), allies[_shownBio].GetComponent<UnitStatScript>().tex);
                //Draw the ally's stats.
                GUI.Box(new Rect(Screen.width * 0.75f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * .35f), "Example stats\nName\nWeight\nLength\nPersonality\nOffense\nDefense\nUtility", textOnly);
                //Draw the ally's description.
                GUI.Box(new Rect(Screen.width * 0.54f, Screen.height * 0.2f + Screen.height * .32f, Screen.width * 0.425f, Screen.height - (Screen.height * 0.25f)), allies[_shownBio].GetComponent<UnitStatScript>().description, textOnly);
            }
            else
            {
                //Draw the enemy's button texture.
                GUI.DrawTexture(new Rect(Screen.width * 0.58f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * .35f), enemies[_shownBio].GetComponent<EnemyStatScript>().tex);
                //Draw the enemy's stats.
                GUI.Box(new Rect(Screen.width * 0.75f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * .35f), "Example stats\nName\nWeight\nLength\nPersonality\nOffense\nDefense\nUtility", textOnly);
                //Draw the enemy's description.
                GUI.Box(new Rect(Screen.width * 0.54f, Screen.height * 0.2f + Screen.height * .32f, Screen.width * 0.425f, Screen.height - (Screen.height * 0.25f)), enemies[_shownBio].GetComponent<EnemyStatScript>().description, textOnly);
            }

        }
        //If the selected fish is locked/null
        else
        {
            //Draw text to tell player that they need to unlock this fish first.
            textOnly.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(Screen.width * 0.585f, Screen.height * 0.2f, Screen.width * 0.36f, Screen.height * (_levelButtonHeight * 4)), "You have to unlock this first.", textOnly);
        }

        for (int i = 0; i < activeList.Length; i++)
        {
            //Determine the bio tile's texture. (Will is be a fish texture or a lock texture?)
            if (activeList[i] == null)
            {
                tex = lockTex;
            }
            else
            {
                //Tests if we are looking at allies or enemies. We need this because ally and enemy textures are pulled from 2 different script types.
                if (isAllies)
                {
                    tex = allies[i].GetComponent<UnitStatScript>().tex;
                }
                else
                {
                    tex = enemies[i].GetComponent<EnemyStatScript>().tex;
                }
            }

            //Determine whether the bio tile is the start of a new row.
            if (i > 0 && i % 3 == 0)
            {
                _row += _levelButtonHeight;
            }

            //Draw the bio tile
            if (GUI.Button(new Rect(Screen.width * _column[i % 3], Screen.height * _row, Screen.width * _levelButtonWidth, Screen.height * _levelButtonHeight), tex, style))
            {
                _shownBio = i;
            }

        }
        #endregion

    }
}
