using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningLauncherScript : MonoBehaviour
{
    public float projectileSpeed = 30.0f;
    public GameObject lightningPrefab;
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 3.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //SoundManagerScript.PlaySound("Arrow");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            if (timeOfInst - airTime + 0.05f < Time.timeSinceLevelLoad)//Stupid to avoid self collision
            {
                CharacterParent cp = other.GetComponent<CharacterParent>();
                if (cp != null)
                {
                    GameObject light = Instantiate(lightningPrefab, this.transform.position, Quaternion.LookRotation(other.transform.position - this.transform.position, Vector3.up));
                    Rigidbody2D rb = light.GetComponent<Rigidbody2D>();
                    rb.AddForce((other.transform.position - this.transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}

