using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSlimeController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    //Rotating Weapon
    public GameObject weaponFromFirePoint;
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
    //Transform for fire point
    public GameObject firePoint;
    //Transform for fire point
    public GameObject firePoint2;
    //Axe Prefab
    public GameObject rockShot;
    //Axe Prefab
    public GameObject rockWallPrefab;
    //Special Attack Speed;
    public float projectileSpeed;//For rock
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

    public bool isAiming = false;

    public int activePrefab;

    //Animation Stuff
    public Animator animator;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    //public bool isDead = false;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    [SerializeField] private AudioSource rockSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.65f;
        maxCharHealth = 225;
        currentCharHealth = 225;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 0.8f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 15.0f;
        specialAttackInput = false;
        specialAttackPause = 16.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }

    void ShootProjectile()
    {
        rockSound.Play();
        
        weaponFromFirePoint.SetActive(false);
        GameObject rock = Instantiate(rockShot, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
    void ShootProjectile2()
    {
        Instantiate(rockWallPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
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
    //Currently not used
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



        //Move Character
        if (KBtime > Time.timeSinceLevelLoad)//Knockback
        {
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else
        {
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }


        //Animate Character
        animator.SetFloat("MoveX", Mathf.Abs(movementInput.x));
        animator.SetFloat("MoveY", Mathf.Abs(movementInput.y));

        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self

        if (!isAiming)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        //Rotate Piv
        //Rotate True Aim Point
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //If you click attack and you can attack, then attack
        if (normalAttackPauseTime + 0.4f < Time.timeSinceLevelLoad + normalAttackPause && isAiming)
        {
            
            ShootProjectile();
            isAiming = false;
            weaponFromFirePoint.SetActive(false);

        }
        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
            if (normalAttackInput)
            {
                weapon.SetActive(false);
                weaponFromFirePoint.SetActive(true);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
                //This is for switching position of swinging weapons
                isAiming = true;
            }

        }

        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            //cannonPlaceholder.SetActive(true);
            if (specialAttackInput)
            {
                //cannonPlaceholder.SetActive(false);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                //Shoot Axe here
                ShootProjectile2();//Place Wall
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