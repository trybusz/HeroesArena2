using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public Rigidbody2D rb;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 3.0f;
        damage = 75; //Plus Bomb damage 75
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null && !this.transform.IsChildOf(cp.transform))
            {
                cp.TakeDamage(damage);
                //Destroy(this.gameObject);
            }
            if (!other.CompareTag("Hitbox") && other != null)
            {
                if (this.transform.parent == null)
                {
                    this.transform.parent = other.transform;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0.0f;
                    rb.isKinematic = true;
                    //rb.Sleep();
                }

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