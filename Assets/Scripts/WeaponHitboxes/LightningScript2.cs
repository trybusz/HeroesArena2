using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript2 : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        damage = 8;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //SoundManagerScript.PlaySound("Arrow");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
                CharacterParent cp = other.GetComponent<CharacterParent>();

                if (cp != null)
                {
                    cp.TakeDamage(damage);
                    cp.TakeStun(0.08f);
                }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

