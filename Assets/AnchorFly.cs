using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorFly : MonoBehaviour
{
    public int damage;
    public float timeOfInst;
    public float airTime;
    public GameObject anchorImage;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 1.0f;
        damage = 25;
        timeOfInst = Time.timeSinceLevelLoad + airTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "ControlPoint" && other.tag != "WaterTag" && other.tag != "BuffZone")
        {
            CharacterParent cp = other.GetComponent<CharacterParent>();
            if (cp != null)
            {
                cp.TakeDamage(damage);
                cp.TakeKnockback(-10.0f, transform.parent.position, 1.0f);
                Instantiate(anchorImage, this.transform.position, this.transform.rotation, other.transform);
            }
            if (!other.CompareTag("Hitbox") && other != null)
            {
                Destroy(gameObject);
            }

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
