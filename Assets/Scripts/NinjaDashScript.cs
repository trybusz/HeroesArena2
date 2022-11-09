using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaDashScript : MonoBehaviour
{
    public int damage = 30;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
