using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArcherController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject BombIndicator;
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
    private float normalAttackPull;
    //Variable to measure cooldown
    private float normalAttackPauseTime;
    //SPECIAL ATTACK
    //Special Attack Speed;
    public float projectileSpeed;
    //Transform for fire point
    public GameObject firePoint;
    //Arrow
    public GameObject ArrowPrefab;
    //Arrow
    public GameObject BombArrowPrefab;
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
    //A Button
    private bool AButtonInput = false;
    //B Button
    private bool BButtonInput = false;
    //X Button
    private bool XButtonInput = false;
    //Y Button
    private bool YButtonInput = false;

    public int activePrefab;

    public GameObject RotatePoint;
    public Animator animator;
    public int BowPosition = 0;//Used to determine whether bow is unloaded (0), loaded (1), or Pulled (2), or Pulled with Bomb (3)
    public int FSM = 0;
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
        charSpeedMod = 0.85f;
        maxCharHealth = 125;
        currentCharHealth = 125;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 1.0f;
        normalAttackPull = 0.5f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 20.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 12.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile()
    {
        GameObject arrow = Instantiate(ArrowPrefab, firePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 45)) * firePoint.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
    void ShootProjectileBomb()
    {
        GameObject bomb = Instantiate(BombArrowPrefab, firePoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 45)) * firePoint.transform.rotation);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
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
    public override void CharSwitching()
    {
        TakeStun(1.0f);
        switchingIndicator.SetActive(true);
        switchingTime = 0.95f + Time.timeSinceLevelLoad;
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

        //Move Character
        if(FSM == 2 || FSM == 3 || FSM == 4 || FSM == 5)
        {
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 0.6f;
        }
        else if (KBtime > Time.timeSinceLevelLoad)
        {
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else
        {
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }


        //Rotate Weapon
        RotatePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        if (FSM == 0 && normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            FSM = 1;
            BowPosition = 1;
        }

        if (normalAttackInput && FSM == 1 )
        {
            //normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            FSM = 2;
            normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPull;
        }

        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            BombIndicator.SetActive(true);
            if (specialAttackInput && FSM == 1)
            {
                FSM = 3;
                BombIndicator.SetActive(false);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPull;
            }
        }
        
        

        if(FSM == 4 && !normalAttackInput)
        {
            FSM = 0;
            BowPosition = 0;
            ShootProjectile();
            normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
        }
        if (FSM == 5 && !specialAttackInput)
        {
            FSM = 0;
            BowPosition = 0;
            ShootProjectileBomb();
            normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
        }

        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            if(FSM == 2)
            {
                FSM = 4;
                BowPosition = 2;
            }
            else if(FSM == 3)
            {
                FSM = 5;
                BowPosition = 3;
            }
        }
        
        //Set The Bow Position
        if(BowPosition == 0)
        {
            weapon.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
        }
        else if (BowPosition == 1)
        {
            weapon.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
        }
        else if (BowPosition == 2)
        {
            weapon.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            weapon4.SetActive(false);
        }
        else if (BowPosition == 3)
        {
            weapon.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(true);
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