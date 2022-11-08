using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 3.0f;
        damage = 75;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterParent cp = other.GetComponent<CharacterParent>();
        if (cp != null)
        {
            cp.TakeDamage(damage);
        }
        Destroy(gameObject);
        //object1.transform.parent = object2.transform
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
