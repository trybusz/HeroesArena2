using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordScript : MonoBehaviour
{
    public int damage = 45;
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
                cp.TakeKnockback(20.0f, transform.parent.position, 0.1f);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
