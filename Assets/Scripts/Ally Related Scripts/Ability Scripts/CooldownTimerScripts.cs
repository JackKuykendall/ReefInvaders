using UnityEngine;
using System.Collections;

public class CooldownTimerScripts : MonoBehaviour
{
    public GameObject cooldownTimer;
    public Material[] _cooldownMats = new Material[9];
    public float counter = 0;
    public float coolDown;
    protected bool canFire = true;
    

	// Use this for initialization
    void Start()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - .02f);
        //Instantiate(cooldownTimer, pos, transform.rotation);
        cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[8];
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
        if (counter > coolDown * .8888f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[0];
        }

        else if (counter > coolDown * .7777f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[1];
        }

        else if (counter > coolDown * .6666f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[2];
        }

        else if (counter > coolDown * .5555f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[3];
        }

        else if (counter > coolDown * .4444f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[4];
        }

        else if (counter > coolDown * .3333f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[5];
        }

        else if (counter > coolDown * .2222f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[6];
        }

        else if (counter > coolDown * .1111f)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[7];
        }

        else if (counter <= 0)
        {
            cooldownTimer.GetComponent<Renderer>().material = _cooldownMats[8];
            canFire = true;
        }

        counter -= Time.deltaTime;
    }
}

