using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class charControl : CharacterParent
{
    //For character switching
    public GameObject switchingIndicator;
    private float switchingTime;
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weaponHitbox;
    //Point to Rotate stuff around
    public GameObject rotatePoint;
    //Swing Anim
    public GameObject swingAnim;
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
    //For swinging weapons
    private bool weaponPos;
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
    public GameObject firePoint;
    //Axe Prefab
    public GameObject axePrefab;
    //Special Attack Button
    private bool specialAttackInput;
    //How long of a cooldown on normal attack
    private static float specialAttackPause;
    //Variable to measure cooldown on special attack
    private float specialAttackPauseTime;
    //Axe On Character Object
    public GameObject axePlaceholder;
    //Angle to hold weapon, set by joystick
    float angle = 0.0f;
    //Forward Angle, set by joystick
    float swordAngle = 0.0f;
    float lastSwordAngle = 0.0f;
    //Previous angle, to be used as a temp on angle
    float lastAngle = 0.0f;


    public int activePrefab;

    public SpriteRenderer primAbilInd;
    public SpriteRenderer secAbilInd;

    //Animation Stuff
    public Animator animator;
    //public bool isDead = false;

    //public bool isStunned = false;
    public float stunTime = 0.0f;

    public float KBtime = 0.0f;
    float charKnockback;
    Vector3 otherPos = Vector3.zero;

    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource axeSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        charSpeed = 5.0f;
        charSpeedMod = 0.75f;
        maxCharHealth = 175;
        currentCharHealth = 175;
        movementInput = Vector2.zero;
        aimInput = Vector2.zero;
        weaponPos = false;
        normalAttackInput = false;
        normalAttackPause = 0.7f;
        normalAttackPauseTime = 0.0f;
        projectileSpeed = 10.0f;
        specialAttackInput = false;
        specialAttackPause = 4.0f;
        specialAttackPauseTime = 0.0f;
        isDead = false;
}
    
    void ShootProjectile()
    {
        axePlaceholder.SetActive(false);
        GameObject axe = Instantiate(axePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
        axeSound.Play();
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

            normalAttackInput = false;

            specialAttackInput = false;
        }

        if (stunTime < Time.timeSinceLevelLoad)
        {
            isStunned = false;
        }



        //Move Character
        if (KBtime > Time.timeSinceLevelLoad)
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
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, swordAngle + 45.0f));
        //Rotate Self
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Pivot Point
        //Rotate Pivot Point
        rotatePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, swordAngle));
        //Rotate True Aim Point
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //If you click attack and you can attack, then attack
        if (normalAttackPauseTime + 0.15 < Time.timeSinceLevelLoad + normalAttackPause)
        {
            weaponHitbox.SetActive(false);

        }
        else if (normalAttackPauseTime + 0.10 < Time.timeSinceLevelLoad + normalAttackPause && (float)currentCharHealth / (float)maxCharHealth < 0.5)
        {
            weaponHitbox.SetActive(true);

        }
        else if (normalAttackPauseTime + 0.05 < Time.timeSinceLevelLoad + normalAttackPause)
        {
            weaponHitbox.SetActive(false);
            swingAnim.SetActive(false);
            
        }
        
        if (normalAttackPauseTime - 0.05f < Time.timeSinceLevelLoad)
        {
            weaponPos = false;
        }
        if (normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            
            if (normalAttackInput)
            {
                swordSound.Play();
                weaponHitbox.SetActive(true);
                swingAnim.SetActive(true);
                //Set time till next attack
                normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
                //This is for switching position of swinging weapons
                weaponPos = true;
            }

        }

        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            axePlaceholder.SetActive(true);
            if (specialAttackInput)
            {
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
            swordAngle = angle;
        }
        else 
        {
            angle = lastAngle;
            swordAngle = lastSwordAngle;
        }
        //Set last angle to be angle
        lastAngle = angle;
        lastSwordAngle = swordAngle;
        //For the swing of a weapon
        if (weaponPos)//save weapon position and esit that
        {
            swordAngle += 45;
        }
        else
        {
            swordAngle -= 45;
        }

        if (currentCharHealth > maxCharHealth)
        {
            currentCharHealth = maxCharHealth;
        }
        primAbilInd.color = new Color(1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f - GetPrimary(), 1.0f);
        secAbilInd.color = new Color(1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f - GetSpecial(), 1.0f);
    }
}