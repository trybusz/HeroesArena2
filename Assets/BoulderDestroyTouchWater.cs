using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDestroyTouchWater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WaterTag")
        {
            Destroy(this.transform.parent.gameObject);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
