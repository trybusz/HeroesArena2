using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinHeal : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = -75;//Every 4 seconds

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
            }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
