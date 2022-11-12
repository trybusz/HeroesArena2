using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHealPulseHit : MonoBehaviour
{
    public int damage = -10;
    public float timeOfInst;
    public float airTime = 0.1f;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
            }
    }
    void Start()
    {
        timeOfInst = Time.timeSinceLevelLoad + airTime;
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
