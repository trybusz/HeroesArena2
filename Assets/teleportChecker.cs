using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.tag != "ControlPoint")
        {
            this.GetComponentInParent<MagicianController>().teleport = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
