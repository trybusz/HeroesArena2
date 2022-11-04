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
        //Debug.Log(other != this.transform.root);
        //if (other != this.transform.root)//Check so self can't take damage
        //{
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null && cp != this.transform.root)
            {
                cp.TakeDamage(damage);
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
