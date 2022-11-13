using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : CharacterParent

{

    public float currentCrateHealth;
    public float maxCrateHealth = 200.0f;



    // Start is called before the first frame update
    void Start()
    {
        currentCrateHealth = maxCrateHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void TakeDamage(int damage)
    {
        if(damage > 0)
        {
            currentCrateHealth -= damage;
        }


        if (currentCrateHealth <= 0)
        {
            //Destroy is temporary
            Destroy(gameObject);
            SoundManagerScript.PlaySound("boxBreak");
        }
    }
    public override void TakeKnockback(float knockback, Vector3 KBPosition, float duration)
    {
    }





}
