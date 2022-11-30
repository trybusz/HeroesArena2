using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerSwing : MonoBehaviour
{
    public int damage = 30;
    //public float timeOfInst;
    //public float airTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        damage = 30;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterParent cp = other.GetComponent<CharacterParent>();
        if (cp != null && !this.transform.IsChildOf(cp.transform))
        {
            cp.TakeDamage(damage);
            cp.TakeKnockback(15.0f, transform.parent.position, 0.2f);
            this.GetComponentInParent<PaladinController>().shouldDoubleHeal = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
