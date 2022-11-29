using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    public GameObject GravityVortexx;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //SoundManagerScript.PlaySound("Arrow");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Instantiate(GravityVortexx, this.transform.position, this.transform.rotation);
                Destroy(gameObject);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Instantiate(GravityVortexx, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
}
