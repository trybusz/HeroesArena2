using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlimeController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject healIndicator;
    //Player RigidBody
    private Rigidbody2D rb;
    //Base character speed
    public float charSpeed;
    //Modifier for how fast each character is relatively
    public float charSpeedMod;
    //Base Character Health
    public int maxCharHealth;
    //Current Health
    public int currentCharHealth;
    //Movement input
    private Vector2 movementInput;
    //Aim Imput
    private Vector2 aimInput;
    //NORMAL ATTACK
    //Normal Attack Button
    private bool normalAttackInput;
    //How long of a cooldown on normal attack
    private float normalAttackPause;
    //Variable to measure cooldown
    private float normalAttackPauseTime;
    //SPECIAL ATTACK
    //Special Attack Speed;
    public float projectileSpeed;
    //Transform for fire point
    public GameObject firePoint1;
    //Axe Prefab
    public GameObject waterBlastPrefab;
    //Axe Prefab
    public GameObject waterHealingRing;
    //Special Attack Button
    private bool specialAttackInput;
    //How long of a cooldown on normal attack
    private static float specialAttackPause;
    //Variable to measure cooldown on special attack
    private float specialAttackPauseTime;
    //Angle to hold weapon, set by joystick
    float angle = 0.0f;
    //Previous angle, to be used as a temp on angle
    float lastAngle = 0.0f;

    public int projectileCounter = 0;

    public int activePrefab;

    public Animator animator;

    public bool isDead = false;

    public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.65f;
        maxCharHealth = 200;
        currentCharHealth = 200;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 1.2f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 10.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 15.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile()
    {
        weapon.SetActive(false);
        GameObject water = Instantiate(waterBlastPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb = water.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
    void ShootProjectile2()//Drops Heal
    {
        healIndicator.SetActive(false);
        Instantiate(waterHealingRing, this.transform.position, this.transform.rotation);
    }

    public override void TakeDamage(int damage)
    {
        currentCharHealth -= damage;

        if (currentCharHealth <= 0)
        {
            //Destroy is temporary
            Destroy(gameObject);
            isDead = true;
            //This vvv Stuff happens in char master
            //Set Position to Spawn
            //Change to new character
        }
    }
    public override void TakeStun(float duration)
    {
        isStunned = true;
        stunTime = duration + Time.timeSinceLevelLoad;
    }
    public override void TakeKnockback(float knockback, Vector3 KBPosition, float duration)
    {
        KBtime = Time.timeSinceLevelLoad + duration;
        charKnockback = knockback;
        otherPos = KBPosition;
    }
    public override void CharSwitching()
    {
        TakeStun(1.0f);
        switchingIndicator.SetActive(true);
        switchingTime = 0.95f + Time.timeSinceLevelLoad;
    }

    public override float GetHealth()
    {
        return ((float)currentCharHealth / (float)maxCharHealth);
    }
    public override float GetPrimary()
    {
        float GPvalue = (normalAttackPauseTime - Time.timeSinceLevelLoad) / normalAttackPause;
        if (GPvalue < 0)
        {
            GPvalue = 0;
        }
        return 1 - GPvalue;
    }
    public override float GetSpecial()
    {
        float GSvalue = (specialAttackPauseTime - Time.timeSinceLevelLoad) / specialAttackPause;
        if (GSvalue < 0)
        {
            GSvalue = 0;
        }
        return 1 - GSvalue;
    }

    void Update()
    {
        //ForScoring
        if (onCtrlPoint && scoreTime < Time.timeSinceLevelLoad)
        {
            scoreTime = Time.timeSinceLevelLoad + 1.0f;
            thisCharScore++;
        }

        //For Switching
        if (switchingTime < Time.timeSinceLevelLoad)
        {
            switchingIndicator.SetActive(false);
        }

        if (!isStunned)
        {
            movementInput = GetComponentInParent<PlayerMaster>().movementInput;

            aimInput = GetComponentInParent<PlayerMaster>().aimInput;

            normalAttackInput = GetComponentInParent<PlayerMaster>().normalAttackInput;

            specialAttackInput = GetComponentInParent<PlayerMaster>().specialAttackInput;
        }
        else
        {
            movementInput = Vector2.zero;

            aimInput = Vector2.zero;

            specialAttackInput = false;

            normalAttackInput = false;
        }

        if (stunTime < Time.timeSinceLevelLoad)
        {
            isStunned = false;
        }

        //update health



        //Animate Character
        animator.SetFloat("MoveX", Mathf.Abs(movementInput.x));
        animator.SetFloat("MoveY", Mathf.Abs(movementInput.y));

        //Move Character
        if (KBtime > Time.timeSinceLevelLoad)
        {
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else
        {
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        firePoint1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Make character dash


        if (normalAttackPauseTime + 1.0 < Time.timeSinceLevelLoad + normalAttackPause && projectileCounter == 5)
        {
            ShootProjectile();
            projectileCounter = 0;
        }
        else if (normalAttackPauseTime + 0.8 < Time.timeSinceLevelLoad + normalAttackPause && projectileCounter == 4)
        {
            ShootProjectile();
            projectileCounter++;
        }
        else if (normalAttackPauseTime + 0.6 < Time.timeSinceLevelLoad + normalAttackPause && projectileCounter == 3)
        {
            ShootProjectile();
            projectileCounter++;
        }
        else if (normalAttackPauseTime + 0.40 < Time.timeSinceLevelLoad + normalAttackPause && projectileCounter == 2)
        {
            ShootProjectile();
            projectileCounter++;
        }
        else if (normalAttackPauseTime + 0.20 < Time.timeSinceLevelLoad + normalAttackPause && projectileCounter == 1)
        {
            ShootProjectile();
            projectileCounter++;
        }



        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
            if (normalAttackInput)
            {
                projectileCounter = 1;
                weapon.SetActive(false);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            if (specialAttackInput)
            {
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                //Shoot Axe here
                ShootProjectile2();
            }
        }


        //Set angle of aim
        if (aimInput.x != 0 && aimInput.y != 0)
        {
            angle = Mathf.Atan2(-aimInput.x, aimInput.y) * Mathf.Rad2Deg;
        }
        else
        {
            angle = lastAngle;
        }
        //Set last angle to be angle
        lastAngle = angle;
        //For the swing of a weapon

        if (currentCharHealth > maxCharHealth)
        {
            currentCharHealth = maxCharHealth;
        }
    }
}