using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierIsTriggerswitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > 5.0f && Time.timeSinceLevelLoad < 5.1f)
        {
            this.GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
