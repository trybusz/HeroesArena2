using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPullDam : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.05f;
        damage = 1;
        timeOfInst = Time.timeSinceLevelLoad + airTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
                cp.TakeKnockback(-5.0f, this.transform.position, 0.05f);
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}