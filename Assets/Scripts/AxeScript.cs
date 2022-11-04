using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    public int axeDamage = 35;
    public float timeOfInst;
    public float airTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (timeOfInst - airTime + 0.0125f < Time.timeSinceLevelLoad)//Stupid to avoid self collision
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(axeDamage);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.5f, Space.Self);
        if(timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
