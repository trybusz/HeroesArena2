using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(this.transform.position, Vector3.forward, 180 * Time.deltaTime);
    }
}
