using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlimeScript : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject dashIndicator;
    public GameObject tornadoPrefab;
    public GameObject firepoint1;
    public GameObject firepoint2;
    public float projectileSpeed;
    public bool dashing;
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

    public Animator animator;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    //public bool isDead = false;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;
    private void Start()
    {
        projectileSpeed = 7.0f;
        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        dashing = false;
        charSpeedMod = 1.0f;
        maxCharHealth = 100;
        currentCharHealth = 100;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 0.7f;
        normalAttackPauseTime = 0.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 1.5f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile()
    {
        GameObject tornado1 = Instantiate(tornadoPrefab, firepoint1.transform.position, firepoint1.transform.rotation);
        Rigidbody2D rb1 = tornado1.GetComponent<Rigidbody2D>();
        rb1.AddForce(firepoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        GameObject tornado2 = Instantiate(tornadoPrefab, firepoint2.transform.position, firepoint2.transform.rotation);
        Rigidbody2D rb2 = tornado2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firepoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }

    public override void TakeDamage(int damage)
    {
        currentCharHealth -= damage;

        if (currentCharHealth <= 0)
        {
            //Destroy is temporary
            //Destroy(gameObject);
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
        //For Switch Indicator
        if (isSwitching)
        {
            switchingIndicator.SetActive(true);
        }
        else
        {
            switchingIndicator.SetActive(false);
        }

        //ForScoring
        if (onCtrlPoint && scoreTime < Time.timeSinceLevelLoad)
        {
            scoreTime = Time.timeSinceLevelLoad + 1.0f;
            thisCharScore++;
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

            normalAttackInput = false;

            specialAttackInput = false;
        }

        if (stunTime < Time.timeSinceLevelLoad)
        {
            isStunned = false;
        }

        //Animate Character
        animator.SetFloat("MoveX", Mathf.Abs(movementInput.x));
        animator.SetFloat("MoveY", Mathf.Abs(movementInput.y));

        if (specialAttackPauseTime + 0.10f > Time.timeSinceLevelLoad + specialAttackPause)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }


        //Move Character
        if (dashing)
        {
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 5;
        }
        else if (KBtime > Time.timeSinceLevelLoad)
        {//For knockback
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
        //Make character dash

        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
            weapon2.SetActive(true);
            if (normalAttackInput)
            {
                ShootProjectile();
                weapon.SetActive(false);
                weapon2.SetActive(false);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            dashIndicator.SetActive(true);
            if (specialAttackInput && !weapon.activeSelf)
            {
                dashIndicator.SetActive(false);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                //Shoot Axe here
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