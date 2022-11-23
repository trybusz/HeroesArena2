using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject weapon3;
    public int weaponPos = 1;
    public GameObject Hitbox1;
    public GameObject Hitbox2;
    public GameObject Hitbox3;
    public GameObject Hitbox4;
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

    public int projectileCounter = 0;

    public int activePrefab;

    public Animator animator;

    //public bool isDead = false;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    //Character specific
    public bool countering = false;
    public bool counterBlock = false;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.9f;
        maxCharHealth = 175;
        currentCharHealth = 175;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 0.6f;
        normalAttackPauseTime = 0.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 5.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    public override void TakeDamage(int damage)
    {
        if (!countering)
        {
            currentCharHealth -= damage;
        }
        else
        {
            counterBlock = true;
        }
        

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
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        //Make character dash


        if (normalAttackPauseTime + 0.1 < Time.timeSinceLevelLoad + normalAttackPause && !countering)
        {
            weaponPos = 1;
            Hitbox1.SetActive(false);
            Hitbox2.SetActive(false);
        }
        if (normalAttackPauseTime < Time.timeSinceLevelLoad && !countering && !counterBlock)
        {
            if (normalAttackInput)
            {
                weaponPos = 2;
                Hitbox1.SetActive(true);
                Hitbox2.SetActive(true);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }

        //counterBlock = false;
        if (specialAttackPauseTime + 0.6 < Time.timeSinceLevelLoad + specialAttackPause && counterBlock)
        {
            weaponPos = 1;
            Hitbox3.SetActive(false);
            Hitbox4.SetActive(false);
            counterBlock = false;
        }
            if (specialAttackPauseTime + 0.5 < Time.timeSinceLevelLoad + specialAttackPause && countering)
        {
            countering = false;
            if (counterBlock)
            {
                Hitbox3.SetActive(true);
                Hitbox4.SetActive(true);
                weaponPos = 2;
            }
        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            if (specialAttackInput)
            {
                weaponPos = 3;
                countering = true;
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;

            }
        }

        if (weaponPos == 1)
        {
            weapon.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
        }
        else if (weaponPos == 2)
        {
            weapon.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
        }
        else
        {
            weapon.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
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