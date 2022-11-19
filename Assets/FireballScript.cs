using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 3.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
            //SoundManagerScript.PlaySound("Bomb");
            Destroy(gameObject);
        }
    }
}