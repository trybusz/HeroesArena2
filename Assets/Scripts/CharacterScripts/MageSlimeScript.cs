using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSlimeScript : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
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
    public GameObject lightningPrefab;
    //Axe Prefab
    public GameObject barrierPrefab;
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

    public SpriteRenderer primAbilInd;
    public SpriteRenderer secAbilInd;

    public Animator animator;

    //public bool isDead = false;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    [SerializeField] private AudioSource lightningSound;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.75f;
        maxCharHealth = 125;
        currentCharHealth = 125;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 1.0f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 30.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 10.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile()
    {
        weapon.SetActive(false);
        GameObject light = Instantiate(lightningPrefab, firePoint1.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)) * firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        lightningSound.Play();
    }
    void ShootProjectile2()
    {
            Instantiate(barrierPrefab, firePoint1.transform.position, Quaternion.Euler(new Vector3(0, 0, -45)) * firePoint1.transform.rotation);
    }

    public override void TakeDamage(int damage)
    {
        if (!onSpawn)
        {
            currentCharHealth -= damage;
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
        firePoint1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Make character dash



        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
            if (normalAttackInput)
            {
                ShootProjectile();
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            if (specialAttackInput)
            {
                ShootProjectile2();
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

        if (currentCharHealth > maxCharHealth)
        {
            currentCharHealth = maxCharHealth;
        }
        primAbilInd.color = new Color(1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f);
        secAbilInd.color = new Color(1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f);
    }
}