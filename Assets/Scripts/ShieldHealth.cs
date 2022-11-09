using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : CharacterParent
{
    public int currentShieldHealth;
    public int maxShieldHealth = 300;
    public int takeDamage = 0;
    public bool needDamage = false;
    public bool needDamage2 = false;
    public float pauseForDamage = 0;


    public override void TakeDamage(int damage)
    {
        takeDamage -= damage;

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = maxShieldHealth;
            gameObject.SetActive(false);
        }
    }
    public override void TakeKnockback(float knockback, Vector3 KBPosition, float duration)
    {
        //No Knockback
    }
    // Start is called before the first frame update
    void Start()
    {
        maxShieldHealth = 300;
        currentShieldHealth = maxShieldHealth;
}

    // Update is called once per frame
    void Update()
    {
        if (needDamage2)
        {
            currentShieldHealth += takeDamage;
            takeDamage = 0;
            needDamage2 = false;
        }
        if (takeDamage != 0)
        {
            needDamage = true;
        }
        if (needDamage)
        {
            needDamage = false;
            needDamage2 = true;
        }

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = maxShieldHealth;
            gameObject.SetActive(false);
        }

    }
}
