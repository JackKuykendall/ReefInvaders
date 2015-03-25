using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

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
        Destroy(this.gameObject);
    }

    public void ChangeToLevel(string scene)
    {
        DontDestroyOnLoad(this.gameObject);
        if(scene.Contains("World1"))
        {
         musicManager.ChangeMusicToGame(menuMusic, gameMusic, 0);
        }
        else if (scene.Contains("World2"))
        {
            musicManager.ChangeMusicToGame(menuMusic, gameMusic, 1);
        }
        else if (scene.Contains("World3"))
        {
            musicManager.ChangeMusicToGame(menuMusic, gameMusic, 2);
        }

        DontDestroyOnLoad(this.gameObject);
        AudioManager.Click();      
        Application.LoadLevel(scene);

    }



    public void Quit()
    {
        AudioManager.Click();
        Application.Quit();
    }
}
