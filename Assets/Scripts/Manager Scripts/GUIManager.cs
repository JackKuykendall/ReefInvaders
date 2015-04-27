using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

    public static int worldIndex = 1;

    private AudioSource menuMusic;
    private AudioSource gameMusic;
    private MusicManager musicManager;


    public static bool created = false;
    void Start()
    {


        musicManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicManager>();
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
        gameMusic = GameObject.FindGameObjectWithTag("GameMusic").GetComponent<AudioSource>();

    }
    public void ChangeToMenu(string scene)
    {
        AudioManager.Click();
        musicManager.ChangeMusicToMenu(gameMusic, menuMusic);
        Application.LoadLevel(scene);
    }

    public void ChangeToLevel(string scene)
    {
        DontDestroyOnLoad(this.gameObject);

        musicManager.ChangeMusicToGame(menuMusic, gameMusic, worldIndex - 1);

        AudioManager.Click();
        Application.LoadLevel("World" + worldIndex + scene);

    }
    public void ChangeWorld(bool Up)
    {
        //See if we should be going up or down
        if (Up)
        {
            //change me to the next world(Hardcoded cancer)
            switch (worldIndex)
            {
                case 1:
                    worldIndex = 2;
                    break;
                case 2:
                    worldIndex = 3;
                    break;
                case 3:
                    worldIndex = 1;
                    break;
            }
        }
        else
        {
            switch (worldIndex)
            {
                case 1:
                    worldIndex = 3;
                    break;
                case 2:
                    worldIndex = 1;
                    break;
                case 3:
                    worldIndex = 2;
                    break;
            }
        }
    }

    public void ReplayLevel()
    {
        Application.LoadLevel(Application.loadedLevel);

    }



    public void Quit()
    {
        AudioManager.Click();
        Application.Quit();
    }
}
