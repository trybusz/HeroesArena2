using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityVortex : MonoBehaviour
{
    //public int damage = -10;
    public float timeOfInst;
    public float airTime;
    public float timeForPull;
    public GameObject GravityPullPulse;

    // Start is called before the first frame update
    void Start()
    {
        timeForPull = Time.timeSinceLevelLoad + 0.05f;
        airTime = 1.01f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeForPull < Time.timeSinceLevelLoad)
        {
            Instantiate(GravityPullPulse, this.transform.position, this.transform.rotation);
            timeForPull = Time.timeSinceLevelLoad + 0.05f;
        }
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
