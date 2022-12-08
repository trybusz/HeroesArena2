using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifedrainScript : MonoBehaviour
{
    public int damage = 20;
    CharacterParent thisParent;
    //public float timeOfInst;
    //public float airTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        damage = 15;
        thisParent = this.GetComponentInParent<CharacterParent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterParent cp = other.GetComponent<CharacterParent>();
        if (cp != null)
        {
            //this.GetComponentInParent()
            if (cp.GetComponent<BoxScript>() == null)
            {
                thisParent.TakeDamage(-damage);
            }

            cp.TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
