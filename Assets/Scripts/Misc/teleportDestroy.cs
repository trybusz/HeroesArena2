using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportDestroy : MonoBehaviour
{
    public float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.timeSinceLevelLoad + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTime < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
