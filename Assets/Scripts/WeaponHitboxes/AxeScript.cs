using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    public int axeDamage;
    public float timeOfInst;
    public float airTime;

    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.8f;
        axeDamage = 35;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            if (timeOfInst - airTime < Time.timeSinceLevelLoad)//Stupid to avoid self collision
            {
                CharacterParent cp = other.GetComponent<CharacterParent>();
                if (cp != null)
                {
                    cp.TakeDamage(axeDamage);
                }
                if (!other.CompareTag("Hitbox") && other != null)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, -1.0f, Space.Self);
        if(timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
