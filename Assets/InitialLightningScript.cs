using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLightningScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public GameObject lightningPrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 3.0f;
        damage = 30;
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
                cp.TakeStun(0.1f);
                
            }
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Instantiate(lightningPrefab, this.transform.position, this.transform.rotation);
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

