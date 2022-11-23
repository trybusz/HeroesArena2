using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaController : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject dashHitbox;
    public GameObject dashIndicator;
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
    //Special Attack Speed;
    public float projectileSpeed;
    //Transform for fire point
    public GameObject firePoint1;
    //Transform for fire point
    public GameObject firePoint2;
    //Transform for fire point
    public GameObject firePoint3;
    //Axe Prefab
    public GameObject starPrefab;
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

    public int activePrefab;

    public Animator animator;

    //public bool isDead = false;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource thwipSound;
    [SerializeField] private AudioClip dash;

    private void Start()
    {
  
        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        dashing = false;
        charSpeed = 5.0f;
        charSpeedMod = 1.0f;
        maxCharHealth = 100;
        currentCharHealth = 100;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        normalAttackInput = false;
        normalAttackPause = 5.0f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 17.0f;
        specialAttackInput = false;
        specialAttackInput = false;
        specialAttackPause = 0.4f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
}
    //Get Inputs
    
    void ShootProjectile()
    {
        thwipSound.Play();
        weapon.SetActive(false);
        GameObject star = Instantiate(starPrefab, firePoint1.transform.position, firePoint1.transform.rotation);
        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint1.transform.up * projectileSpeed, ForceMode2D.Impulse);
        //Star 2
        GameObject star2 = Instantiate(starPrefab, firePoint2.transform.position, firePoint2.transform.rotation);
        Rigidbody2D rb2 = star2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint2.transform.up * projectileSpeed, ForceMode2D.Impulse);
        //Star 3
        GameObject star3 = Instantiate(starPrefab, firePoint3.transform.position, firePoint3.transform.rotation);
        Rigidbody2D rb3 = star3.GetComponent<Rigidbody2D>();
        rb3.AddForce(firePoint3.transform.up * projectileSpeed, ForceMode2D.Impulse);
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
        float GPvalue = (specialAttackPauseTime - Time.timeSinceLevelLoad) / specialAttackPause;
        if (GPvalue < 0)
        {
            GPvalue = 0;
        }
        return 1 - GPvalue;
    }
    public override float GetSpecial()
    {
        float GSvalue =  (normalAttackPauseTime - Time.timeSinceLevelLoad) / normalAttackPause;
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

            //I flipped normal and special input on this character
            specialAttackInput = GetComponentInParent<PlayerMaster>().normalAttackInput;

            normalAttackInput = GetComponentInParent<PlayerMaster>().specialAttackInput;
        }
        else
        {
            movementInput = Vector2.zero;

            aimInput = Vector2.zero;

            //I flipped normal and special input on this character
            specialAttackInput = false;

            normalAttackInput = false;
        }

        if(stunTime < Time.timeSinceLevelLoad)
        {
            isStunned = false;
        }

        //update health



        //Animate Character
        animator.SetFloat("MoveX", Mathf.Abs(movementInput.x));
        animator.SetFloat("MoveY", Mathf.Abs(movementInput.y));

        //Move Character
        if (dashing)
        {

            dashHitbox.SetActive(true);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 10;
        }
        else if(KBtime > Time.timeSinceLevelLoad){
            rb.velocity = new Vector2(rb.transform.position.x - otherPos.x, rb.transform.position.y - otherPos.y).normalized * charKnockback;
        }
        else
        {
            dashHitbox.SetActive(false);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        firePoint1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        firePoint2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 10));
        firePoint3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 10));
        //Make character dash
        if (normalAttackPauseTime + 0.15f > Time.timeSinceLevelLoad + normalAttackPause)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }

        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            dashIndicator.SetActive(true);
            if (normalAttackInput)
            {
                dashSound.PlayOneShot(dash);
                dashIndicator.SetActive(false);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            }
            
        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
            if (specialAttackInput)
            {

                //dashSound.PlayOneShot(dash);
                //Set time till next attack
                specialAttackPauseTime = Time.timeSinceLevelLoad + specialAttackPause;
                //Shoot Axe here
                ShootProjectile();
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