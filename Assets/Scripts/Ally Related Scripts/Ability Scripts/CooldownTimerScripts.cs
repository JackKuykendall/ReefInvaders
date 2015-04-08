using UnityEngine;
using System.Collections;

public class CooldownTimerScripts : MonoBehaviour
{
    public GameObject cooldownTimer;
    private SpriteRenderer spriteRenderer;
    public Sprite[] _cooldownTex = new Sprite[9];
    public Material[] _cooldownMats = new Material[9];
    public float counter = 0;
    public float coolDown;
    protected bool canFire = true;
    

	// Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - .02f);
        //Instantiate(cooldownTimer, pos, transform.rotation);

        spriteRenderer = cooldownTimer.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _cooldownTex[8];
=======
        cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[8];
>>>>>>> origin/Sean's_Branch
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (!SceneManager.isPaused)
        {
            CoolDownUpdate(cooldownTimer);
        }
    }
    public void CoolDownUpdate(GameObject cooldownTimer)
    {
        spriteRenderer = cooldownTimer.GetComponent<SpriteRenderer>();
        if (counter > coolDown * .8888f)
        {
            spriteRenderer.sprite = _cooldownTex[0];
        }

        else if (counter > coolDown * .7777f)
        {
            spriteRenderer.sprite = _cooldownTex[1];
        }

        else if (counter > coolDown * .6666f)
        {
            spriteRenderer.sprite = _cooldownTex[2];
        }

        else if (counter > coolDown * .5555f)
        {
            spriteRenderer.sprite = _cooldownTex[3];
        }

        else if (counter > coolDown * .4444f)
        {
            spriteRenderer.sprite = _cooldownTex[4];
        }

        else if (counter > coolDown * .3333f)
        {
            spriteRenderer.sprite = _cooldownTex[5];
        }

        else if (counter > coolDown * .2222f)
        {
            spriteRenderer.sprite = _cooldownTex[6];
        }

        else if (counter > coolDown * .1111f)
        {
            spriteRenderer.sprite = _cooldownTex[7];
        }

        else if (counter <= 0)
        {
            spriteRenderer.sprite = _cooldownTex[8];
            canFire = true;
        }

        counter -= Time.deltaTime;
    }
}

