using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBladeScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public bool charCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.5f;
        damage = 25;
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
                charCheck = true;
            }
            if (!other.CompareTag("Hitbox") && other != null && charCheck)
            {
                Destroy(gameObject);
            }
            charCheck = false;
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
