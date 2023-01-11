using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject charge1;
    public GameObject charge2;
    public GameObject charge3;
    public GameObject charge4;
    public GameObject charge5;
    private int chargeValue;
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
    public GameObject charge1Prefab;
    //Axe Prefab
    public GameObject charge2Prefab;
    //Axe Prefab
    public GameObject charge3Prefab;
    //Axe Prefab
    public GameObject charge4Prefab;
    //Axe Prefab
    public GameObject charge5Prefab;
    //Axe Prefab
    public GameObject ChargingAnim;
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

    [SerializeField] private AudioSource spellSound;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.75f;
        maxCharHealth = 150;
        currentCharHealth = 150;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 0.5f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 10.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 0.5f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile1()
    {
        charge1.SetActive(false);
        GameObject light = Instantiate(charge1Prefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        spellSound.Play();
    }
    void ShootProjectile2()
    {
        charge2.SetActive(false);
        GameObject light = Instantiate(charge2Prefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * (projectileSpeed + 3.0f), ForceMode2D.Impulse);
        spellSound.Play();
    }
    void ShootProjectile3()
    {
        charge3.SetActive(false);
        GameObject light = Instantiate(charge3Prefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * (projectileSpeed + 6.0f), ForceMode2D.Impulse);
        spellSound.Play();
    }
    void ShootProjectile4()
    {
        charge4.SetActive(false);
        GameObject light = Instantiate(charge4Prefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * (projectileSpeed + 9.0f), ForceMode2D.Impulse);
        spellSound.Play();
    }
    void ShootProjectile5()
    {
        charge5.SetActive(false);
        GameObject light = Instantiate(charge5Prefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb1 = light.GetComponent<Rigidbody2D>();
        rb1.AddForce(firePoint1.transform.up * (projectileSpeed + 12.0f), ForceMode2D.Impulse);
        spellSound.Play();
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
            if (chargeValue == 1)
            {
                charge1.SetActive(true);
                charge2.SetActive(false);
                charge3.SetActive(false);
                charge4.SetActive(false);
                charge5.SetActive(false);
            }
            else if (chargeValue == 2)
            {
                charge1.SetActive(false);
                charge2.SetActive(true);
                charge3.SetActive(false);
                charge4.SetActive(false);
                charge5.SetActive(false);
            }
            else if (chargeValue == 3)
            {
                charge1.SetActive(false);
                charge2.SetActive(false);
                charge3.SetActive(true);
                charge4.SetActive(false);
                charge5.SetActive(false);
            }
            else if (chargeValue == 4)
            {
                charge1.SetActive(false);
                charge2.SetActive(false);
                charge3.SetActive(false);
                charge4.SetActive(true);
                charge5.SetActive(false);
            }
            else if (chargeValue == 5)
            {
                charge1.SetActive(false);
                charge2.SetActive(false); ;
                charge3.SetActive(false);
                charge4.SetActive(false);
                charge5.SetActive(true);
            }

            if (normalAttackInput)
            {
                if (chargeValue == 1)
                {
                    ShootProjectile1();
                }
                else if (chargeValue == 2)
                {
                    ShootProjectile2();
                }
                else if (chargeValue == 3)
                {
                    ShootProjectile3();
                }
                else if (chargeValue == 4)
                {
                    ShootProjectile4();
                }
                else if (chargeValue == 5)
                {
                    ShootProjectile5();
                }
                chargeValue = 1;
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime + 0.45f < Time.timeSinceLevelLoad + specialAttackPause)
        {
            ChargingAnim.SetActive(false);
        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            if (specialAttackInput)
            {
                ChargingAnim.SetActive(true);
                currentCharHealth += 3;
                TakeStun(0.5f);
                if (chargeValue < 5)
                {
                    chargeValue += 1;
                }
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