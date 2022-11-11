using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate1_2 : MonoBehaviour
{
    public int damage = 15;
    //public float timeOfInst;
    //public float airTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterParent cp = other.GetComponent<CharacterParent>();
        if (cp != null && !this.transform.IsChildOf(cp.transform))
        {
            cp.TakeDamage(damage);
            cp.TakeKnockback(3.0f, transform.parent.position, 0.15f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
