using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinStab : MonoBehaviour
{
    public int damage = 100;
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
            cp.TakeStun(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
