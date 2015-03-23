using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public GUIManager guiManager;
    private MusicManager musicManager;

    public static bool isLevel;

    public AudioClip buttonClick;
    public AudioClip swimming;
    public AudioClip money;
    public AudioClip deathLF;

    public AudioClip[] abilities;
    /*
    0 - Snail
    1 - Turtle
    2 - Eel
    3 - Octopus
    */
    public AudioClip[] projectiles;
    /*
    0 - AngelFish
    1 - Snail
    2 - Grouper
    */
    public AudioClip[] damaged;
    /*
    0 - Ally
    1 - Basic Lionfish
    2 - Armored Lionfish
    */



    public static bool created = false;
    static private AudioClip[] abilitiesSTAT;
    static private AudioClip[] projectilesSTAT;
    static private AudioClip[] damagedSTAT;
    static AudioClip Music1;
    static AudioClip Music2;

    static private AudioClip click;
    static private AudioClip swim;
    static private AudioClip moneys;
    static private AudioClip deathLF_Stat;

    static public AudioSource audioPlayer;
    static public AudioSource musicPlayer;
    // Use this for initialization

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

        audioPlayer = GetComponent<AudioSource>();
        guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
        musicManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicManager>();

        click = buttonClick;
        swim = swimming;
        moneys = money;
        deathLF_Stat = deathLF;


        abilitiesSTAT = abilities;
        projectilesSTAT = projectiles;
        damagedSTAT = damaged;
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.isPaused)
        {
            audioPlayer.Pause();
        }
        else
        {
            audioPlayer.Play();
        }
    }

    static public void ChangeMusic(bool music)
    {

    }

    static public void PlaySound(AudioClip sound, float volume, float pitch)
    {
        audioPlayer.volume = volume;
        audioPlayer.pitch = pitch;
        audioPlayer.PlayOneShot(sound);
    }

   
    static public void PlayProjectileSound(int index, float volume, float pitch)
    {
        audioPlayer.volume = volume;
        audioPlayer.pitch = pitch;
        audioPlayer.PlayOneShot(projectilesSTAT[index]);
    }

    static public void PlayAbilitySound(int index, float volume, float pitch)
    {
        audioPlayer.volume = volume;
        audioPlayer.pitch = pitch;
        audioPlayer.PlayOneShot(abilitiesSTAT[index]);
    }

    static public void PlayDamagedSound(int index, float volume, float pitch)
    {
        audioPlayer.volume = volume;
        audioPlayer.pitch = pitch;
        audioPlayer.PlayOneShot(damagedSTAT[index]);
    }

    static public void Click()
    {
        audioPlayer.volume = 1;
        audioPlayer.pitch = 1;
        audioPlayer.PlayOneShot(click);
    }

    static public void LFDeath()
    {
        audioPlayer.volume = 1;
        audioPlayer.pitch = 1;
        audioPlayer.PlayOneShot(deathLF_Stat);
    }

    static public void Swimming()
    {
        audioPlayer.clip = swim;
        audioPlayer.volume = 1;
        audioPlayer.pitch = 1;
        audioPlayer.Play();
    }

    static public void Money()
    {
        audioPlayer.volume = 1;
        audioPlayer.pitch = 1;
        audioPlayer.PlayOneShot(moneys);
    }



}

