using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinScript : CharacterParent
{
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weapon2;
    public GameObject weaponHitbox;
    public GameObject invsIndicator;
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
    //A Button
    private bool AButtonInput = false;
    //B Button
    private bool BButtonInput = false;
    //X Button
    private bool XButtonInput = false;
    //Y Button
    private bool YButtonInput = false;

    public int activePrefab;

    public Animator animator;

    public bool isDead = false;
    private SpriteRenderer spriteR;
    public SpriteRenderer WeaponSprite;
    public SpriteRenderer InvisIndSprite;



    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        dashing = false;
        charSpeed = 5.0f;
        charSpeedMod = 0.9f;
        maxCharHealth = 100;
        currentCharHealth = 100;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 1.0f;
        normalAttackPauseTime = 0.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 10.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
        spriteR = gameObject.GetComponent<SpriteRenderer>();

    }
    //Get Inputs


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
    public override void TakeKnockback(float knockback, Vector3 KBPosition, float duration)
    {
        KBtime = Time.timeSinceLevelLoad + duration;
        charKnockback = knockback;
        otherPos = KBPosition;
    }

    void Update()
    {
        //ForScoring
        if (onCtrlPoint && scoreTime < Time.timeSinceLevelLoad)
        {
            scoreTime = Time.timeSinceLevelLoad + 1.0f;
            thisCharScore++;
        }


        movementInput = GetComponentInParent<PlayerMaster>().movementInput;

        aimInput = GetComponentInParent<PlayerMaster>().aimInput;

        //I flipped normal and special input on this character
        normalAttackInput = GetComponentInParent<PlayerMaster>().normalAttackInput; ;

        specialAttackInput = GetComponentInParent<PlayerMaster>().specialAttackInput;

        //update health



        //Animate Character
        animator.SetFloat("MoveX", Mathf.Abs(movementInput.x));
        animator.SetFloat("MoveY", Mathf.Abs(movementInput.y));

        //Move Character
        if (dashing)
        {
            //Set Invs
            spriteR.enabled = false;
            WeaponSprite.enabled = false;
            InvisIndSprite.enabled = false;
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 1.2f;
        }
        else if (KBtime > Time.timeSinceLevelLoad)
        {
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else
        {
            spriteR.enabled = true;
            WeaponSprite.enabled = true;
            InvisIndSprite.enabled = true;
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 45.0f));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
       
        //Make character dash
        if (specialAttackPauseTime + 2.5f > Time.timeSinceLevelLoad + specialAttackPause)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }

        if (normalAttackPauseTime + 0.05 < Time.timeSinceLevelLoad + normalAttackPause)
        {
            weaponHitbox.SetActive(false);
        }
        if (normalAttackPauseTime - 0.10f < Time.timeSinceLevelLoad)
        {
            weapon2.SetActive(false);
            weapon.SetActive(true);
        }
        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            
            if (normalAttackInput && !dashing)
            {
                weapon2.SetActive(true);
                weapon.SetActive(false);
                weaponHitbox.SetActive(true);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            invsIndicator.SetActive(true);
            if (specialAttackInput)
            {
                invsIndicator.SetActive(false);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
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


    }
}