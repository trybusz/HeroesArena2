using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwashbucklerController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject anchorIndicator;
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
    public float projectileSpeed;//For bullets
    //Special Attack Speed;
    public float projectileSpeed2;//For Anchor
    //Transform for fire point
    public GameObject firePoint0;
    public GameObject firePoint1;
    public GameObject firePoint2;
    public GameObject firePoint3;
    public GameObject firePoint4;
    public GameObject firePoint5;
    public GameObject firePoint6;
    public GameObject firePoint7;
    public GameObject firePoint8;
    //Axe Prefab
    public GameObject bulletPrefab;
    //Axe Prefab
    public GameObject anchorPrefab;
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

    public bool isThrowingAnchor = false;

    public int activePrefab;

    public Animator animator;

    //public bool isDead = false;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    [SerializeField] private AudioSource gunSound;
    [SerializeField] private AudioSource anchorSound;
    [SerializeField] private AudioClip anchor;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.60f;
        maxCharHealth = 200;
        currentCharHealth = 200;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 2.0f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 20.0f;
        projectileSpeed2 = 13.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 7.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
    }
    //Get Inputs

    void ShootProjectile()
    {

        gunSound.Play();
        
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.transform.position, firePoint3.transform.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet4 = Instantiate(bulletPrefab, firePoint4.transform.position, firePoint4.transform.rotation);
            Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
            rb4.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet5 = Instantiate(bulletPrefab, firePoint5.transform.position, firePoint5.transform.rotation);
            Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
            rb5.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet6 = Instantiate(bulletPrefab, firePoint6.transform.position, firePoint6.transform.rotation);
            Rigidbody2D rb6 = bullet6.GetComponent<Rigidbody2D>();
            rb6.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet7 = Instantiate(bulletPrefab, firePoint7.transform.position, firePoint7.transform.rotation);
            Rigidbody2D rb7 = bullet7.GetComponent<Rigidbody2D>();
            rb7.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
            GameObject bullet8 = Instantiate(bulletPrefab, firePoint8.transform.position, firePoint8.transform.rotation);
            Rigidbody2D rb8 = bullet8.GetComponent<Rigidbody2D>();
            rb8.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);

    }
    void ShootProjectile2()//Drops Heal
    {
        anchorIndicator.SetActive(false);
        GameObject anch = Instantiate(anchorPrefab, firePoint0.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)) * firePoint0.transform.rotation, this.transform);
        Rigidbody2D rb = anch.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint0.transform.up * projectileSpeed2, ForceMode2D.Impulse);
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


        if (isThrowingAnchor)
        {

            //anchorSound.PlayOneShot(anchor);

            movementInput = Vector2.zero;

            aimInput = Vector2.zero;

            specialAttackInput = false;

            normalAttackInput = GetComponentInParent<PlayerMaster>().normalAttackInput;

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
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        firePoint0.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        firePoint1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 6.5f));
        firePoint2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 6.5f));
        firePoint3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 19.5f));
        firePoint4.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 19.5f));
        firePoint5.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 32.5f));
        firePoint6.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 32.5f));
        firePoint7.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 45.5f));
        firePoint8.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 45.5f));
        //Make character dash


        if (normalAttackPauseTime - 0.05f < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
        }
        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            
            if (normalAttackInput)
            {
                weapon.SetActive(false);
                ShootProjectile();
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }

        }
        if (specialAttackPauseTime - specialAttackPause + 0.7f > Time.timeSinceLevelLoad)
        {
            isThrowingAnchor = true;
        }
        else
        {
            isThrowingAnchor = false;
        }
            if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            anchorIndicator.SetActive(true);
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


/*
 * if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            rb1.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.transform.position, firePoint3.transform.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet4 = Instantiate(bulletPrefab, firePoint4.transform.position, firePoint4.transform.rotation);
            Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
            rb4.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet5 = Instantiate(bulletPrefab, firePoint5.transform.position, firePoint5.transform.rotation);
            Rigidbody2D rb5 = bullet5.GetComponent<Rigidbody2D>();
            rb5.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet6 = Instantiate(bulletPrefab, firePoint6.transform.position, firePoint6.transform.rotation);
            Rigidbody2D rb6 = bullet6.GetComponent<Rigidbody2D>();
            rb6.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet7 = Instantiate(bulletPrefab, firePoint7.transform.position, firePoint7.transform.rotation);
            Rigidbody2D rb7 = bullet7.GetComponent<Rigidbody2D>();
            rb7.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
        if (Random.Range(-1.0f, 1.0f) > 0.0f)
        {
            GameObject bullet8 = Instantiate(bulletPrefab, firePoint8.transform.position, firePoint8.transform.rotation);
            Rigidbody2D rb8 = bullet8.GetComponent<Rigidbody2D>();
            rb8.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        }
*/