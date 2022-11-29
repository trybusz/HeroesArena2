using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    public float projectileSpeed = 25.0f;
    public GameObject shadowBladePrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.0f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            GameObject cannonBall = Instantiate(shadowBladePrefab, this.transform.position, this.transform.rotation);
            Rigidbody2D rb = cannonBall.GetComponent<Rigidbody2D>();
            rb.AddForce(this.transform.up * projectileSpeed, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
