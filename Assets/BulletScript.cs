using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.15f;
        damage = 20;
        timeOfInst = Time.timeSinceLevelLoad + airTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
            }
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Destroy(gameObject);
            }

        }
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
