using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonFire : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    public bool shot;
    public float projectileSpeed = 25.0f;
    public GameObject firePoint;
    public GameObject cannonBallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 2.5f;
        shot = false;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeOfInst - 0.5f < Time.timeSinceLevelLoad && !shot)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Rigidbody2D rb = cannonBall.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
            shot = true;
            SoundManagerScript.PlaySound("Cannon");
        }
            

        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
