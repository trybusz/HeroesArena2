using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningLauncherScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public float lightTime;
    public GameObject Lightning1;
    public GameObject Lightning2;
    public GameObject Lightning3;
    public GameObject Lightning4;
    public GameObject LightningHitbox;
    public int FSM = 0;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        lightTime = Time.timeSinceLevelLoad + 0.15f;
        //SoundManagerScript.PlaySound("Arrow");

    }



    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
        if (lightTime < Time.timeSinceLevelLoad)
        {
            lightTime = Time.timeSinceLevelLoad + 0.15f;
            if (FSM == 0)
            {
                FSM = 1;
                Lightning1.SetActive(false);
                Lightning2.SetActive(false);
                Lightning3.SetActive(true);
                Lightning4.SetActive(true);
                LightningHitbox.SetActive(true);
            }
            else if (FSM == 1)
            {
                FSM = 2;
                Lightning1.SetActive(false);
                Lightning2.SetActive(false);
                Lightning3.SetActive(false);
                Lightning4.SetActive(false);
                LightningHitbox.SetActive(false);
            }
            else if (FSM == 2)
            {
                FSM = 3;
                Lightning1.SetActive(true);
                Lightning2.SetActive(true);
                Lightning3.SetActive(false);
                Lightning4.SetActive(false);
                LightningHitbox.SetActive(true);
            }
            else
            {
                FSM = 0;
                Lightning1.SetActive(false);
                Lightning2.SetActive(false);
                Lightning3.SetActive(false);
                Lightning4.SetActive(false);
                LightningHitbox.SetActive(false);
            }
        }
            
    }
}

