using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarScript : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;

    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.4f;
        damage = 10;
        timeOfInst = Time.timeSinceLevelLoad + airTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
            }
            if (!other.CompareTag("NinjaStar") && other != null)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.5f, Space.Self);
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Destroy(gameObject);
        }
    }
}
