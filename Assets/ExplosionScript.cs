using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public GameObject Explo1;
    public GameObject Explo2;
    public GameObject Explo3;
    public GameObject Explo4;
    public GameObject Explo5;
    public GameObject Explo6;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.6f;
        damage = 100;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        //this.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterParent cp = other.GetComponent<CharacterParent>();
        if (cp != null)
        {
            cp.TakeDamage(damage);
        }
        //object1.transform.parent = object2.transform
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }

        if (timeOfInst - 0.1f < Time.timeSinceLevelLoad)
        {
            Explo5.SetActive(false);
            Explo6.SetActive(true);
        }
        else if (timeOfInst - 0.2f < Time.timeSinceLevelLoad)
        {
            Explo4.SetActive(false);
            Explo5.SetActive(true);
        }
        else if (timeOfInst - 0.3f < Time.timeSinceLevelLoad)
        {
            Explo3.SetActive(false);
            Explo4.SetActive(true);
        }
        else if (timeOfInst - 0.4f < Time.timeSinceLevelLoad)
        {
            Explo2.SetActive(false);
            Explo3.SetActive(true);
        }
        else if (timeOfInst - 0.5f < Time.timeSinceLevelLoad)
        {
            Explo1.SetActive(false);
            Explo2.SetActive(true);
        }
    }
}
