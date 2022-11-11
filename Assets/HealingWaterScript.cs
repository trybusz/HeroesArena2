using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWaterScript : MonoBehaviour
{
    //public int damage = -10;
    public float timeOfInst;
    public float airTime;
    public float timeForHeal;
    public GameObject healPulse;
    
    // Start is called before the first frame update
    void Start()
    {
        timeForHeal = Time.timeSinceLevelLoad + 1.0f;
        airTime = 10.1f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeForHeal < Time.timeSinceLevelLoad)
        {
            Instantiate(healPulse, this.transform.position, this.transform.rotation);
            timeForHeal = Time.timeSinceLevelLoad + 1.0f;
        }
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
