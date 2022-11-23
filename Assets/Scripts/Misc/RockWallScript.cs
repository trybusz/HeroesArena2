using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWallScript : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 8.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
