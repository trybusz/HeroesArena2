using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject dashHitbox;
    public GameObject weaponDashHitbox;
    public GameObject dashIndicator;
    public GameObject shieldReady;
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
    //Base Shield Health
    public int maxShieldHealth;
    //Current SHield Health
    public int currentShieldHealth;
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
    public SpriteRenderer primAbilInd;
    public SpriteRenderer secAbilInd;

    //public bool isDead = false;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    //[SerializeField] private AudioSource ShieldSound;
    [SerializeField] private AudioSource DashSound;
    [SerializeField] private AudioSource ShieldBreakSound;

    //[SerializeField] private AudioClip shield;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip breaks;



    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        dashing = false;
        charSpeedMod = 0.5f;
        maxCharHealth = 250;
        currentCharHealth = 250;
        maxShieldHealth = 200;
        currentShieldHealth = 200;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 1.0f;
        normalAttackPauseTime = 0.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 15.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs


    public override void TakeDamage(int damage)
    {
        if (!onSpawn)
        {
            if (weapon.activeSelf && damage > 0)
            {
                currentShieldHealth -= damage;
            }
            else
            {
                currentCharHealth -= damage;
            }
        }

        

        if (currentShieldHealth <= 0)
        {
            ShieldBreakSound.PlayOneShot(breaks);
            weapon.SetActive(false);
            currentShieldHealth = maxShieldHealth;
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
        if (stunTime < duration + Time.timeSinceLevelLoad)
        {
            isStunned = true;
            stunTime = duration + Time.timeSinceLevelLoad;
        }
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



        if (onSpawn)
        {
            movementInput = GetComponentInParent<PlayerMaster>().movementInput;

            aimInput = GetComponentInParent<PlayerMaster>().aimInput;

            normalAttackInput = false;

            specialAttackInput = false;
        }
        else if (!isStunned)
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

        if (normalAttackPauseTime + 0.10f > Time.timeSinceLevelLoad + normalAttackPause)
        {
            dashing = true;
        }
        else if (normalAttackPauseTime + 0.15f > Time.timeSinceLevelLoad + normalAttackPause && weapon.activeSelf)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }


        //Move Character
        if (KBtime > Time.timeSinceLevelLoad)
        {//For knockback
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else if (dashing)
        {
            //DashSound.PlayOneShot(dash);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 4;
        }
        else
        {
            dashHitbox.SetActive(false);
            weaponDashHitbox.SetActive(false);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Make character dash
        

        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            dashIndicator.SetActive(true);
            if (normalAttackInput)
            {
                if (weapon.activeSelf)
                {
                    weaponDashHitbox.SetActive(true);
                }
                else
                {
                    dashHitbox.SetActive(true);
                }
                dashIndicator.SetActive(false);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            shieldReady.SetActive(true);
            if (specialAttackInput && !weapon.activeSelf)
            {
                shieldReady.SetActive(false);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                //Shoot Axe here

                weapon.SetActive(true);
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
        primAbilInd.color = new Color(1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f);
        secAbilInd.color = new Color(1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f);
    }
}