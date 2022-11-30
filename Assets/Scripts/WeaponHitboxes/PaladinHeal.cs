using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinHeal : MonoBehaviour
{
    public int damage;
    public int damage2;
    // Start is called before the first frame update
    void Start()
    {
        damage = -50;
        damage2 = -100;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int isDoubled = GetComponentInParent<PaladinController>().shouldDoubleHeal;
        CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                if (isDoubled == 1)
                {
                    cp.TakeDamage(damage2);
                }
                else
                {
                    cp.TakeDamage(damage);
                }
                
            }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
