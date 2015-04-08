using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] worlds = new AudioClip[3];

    private AudioSource musicTo;
    private AudioSource musicFrom;
    private bool crossFade;

    // Use this for initialization
    void Awake()
    {
        crossFade = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (crossFade)
        {
            if (musicFrom.volume > .01f)
            {
                musicFrom.volume = Mathf.Lerp(musicFrom.volume, -.1f, .05f);
            }
            if (musicTo.volume < .9f)
            {
                musicTo.volume = Mathf.Lerp(musicTo.volume, 1.1f, .05f);
            }
            else if (musicFrom.volume < .01f && musicTo.volume > .9f)
            {
                musicFrom.volume = 0f;
                musicTo.volume = 1f;
                crossFade = false;
            }
        }
    }
    //This is primarily for switching from gameplay to menu.
    public void ChangeMusicToMenu(AudioSource from, AudioSource to)
    {

        musicFrom = from;
        musicTo = to;
        crossFade = true;

    }
    //This function should be used to change the gameplay music before it fades in.
    //This is meant to accomodate for each world having its own background music.
    public void ChangeMusicToGame(AudioSource from, AudioSource to, int world)
    {

        musicFrom = from;
        musicTo = to;
        if (worlds[world] != null)
        {
            musicTo.clip = worlds[world];
        }

        crossFade = true;

    }



}
