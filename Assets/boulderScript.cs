using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public float timeSinceStart;
    public float timeTimesDamage;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 4.0f;
        damage = 50;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //SoundManagerScript.PlaySound("Arrow");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "BuffZone")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage + (int)timeTimesDamage);
                cp.TakeKnockback(20.0f * (1 + timeSinceStart/airTime), this.transform.position, 0.10f);
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
        timeSinceStart = Time.timeSinceLevelLoad + airTime - timeOfInst;
        timeTimesDamage = timeSinceStart * 25;
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
