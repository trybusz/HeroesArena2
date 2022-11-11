using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlastScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
                cp.TakeKnockback(15.0f, this.transform.position, 0.15f);
            }
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Destroy(gameObject);
            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.0f;
        damage = 10;
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
