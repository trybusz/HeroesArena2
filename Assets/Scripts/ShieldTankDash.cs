using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTankDash : MonoBehaviour
{
    public int damage = 70;
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
            cp.TakeKnockback(27.5f, transform.parent.position, 0.20f);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}