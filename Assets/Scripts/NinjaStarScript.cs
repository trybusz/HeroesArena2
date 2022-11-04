using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarScript : MonoBehaviour
{
    public int damage = 10;
    public float timeOfInst;
    public float airTime = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        timeOfInst = Time.timeSinceLevelLoad + airTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (timeOfInst - airTime + 0.025f < Time.timeSinceLevelLoad)//Stupid to avoid self collision
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.5f, Space.Self);
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
