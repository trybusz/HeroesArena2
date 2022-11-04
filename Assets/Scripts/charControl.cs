using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class charControl : CharacterParent
{
    //Rotating Weapon
    public GameObject weapon;
    public GameObject weaponHitbox;
    //Point to Rotate stuff around
    public GameObject rotatePoint;
    //Point to Rotate Weapon Around
    public GameObject swordRotatePoint;
    //Player RigidBody
    private Rigidbody2D rb;
    //Base character speed
    public float charSpeed = 4.0f;
    //Modifier for how fast each character is relatively
    public float charSpeedMod = 0.75f;
    //Base Character Health
    public int maxCharHealth = 150;
    //Current Health
    public int currentCharHealth = 150;
    //Movement input
    private Vector2 movementInput = Vector2.zero;
    //Aim Imput
    private Vector2 aimInput = Vector2.zero;
    //NORMAL ATTACK
    //For swinging weapons
    private bool weaponPos = false;
    //Normal Attack Button
    private bool normalAttackInput = false;
    //How long of a cooldown on normal attack
    private float normalAttackPause = 0.5f;
    //Variable to measure cooldown
    private float normalAttackPauseTime = 0.0f;
    //SPECIAL ATTACK
    //Special Attack Speed;
    public float projectileSpeed = 10.0f;
    //Transform for fire point
    public GameObject firePoint;
    //Axe Prefab
    public GameObject axePrefab;
    //Special Attack Button
    private bool specialAttackInput = false;
    //How long of a cooldown on normal attack
    private static float specialAttackPause = 4.0f;
    //Variable to measure cooldown on special attack
    private float specialAttackPauseTime = 0.0f;
    //Axe On Character Object
    public GameObject axePlaceholder;
    //Angle to hold weapon, set by joystick
    float angle = 0.0f;
    //Forward Angle, set by joystick
    float swordAngle = 0.0f;
    float lastSwordAngle = 0.0f;
    //Previous angle, to be used as a temp on angle
    float lastAngle = 0.0f;
    //A Button
    public bool AButtonInput = false;
    //B Button
    public bool BButtonInput = false;
    //X Button
    public bool XButtonInput = false;
    //Y Button
    public bool YButtonInput = false;

    public int activePrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void ShootProjectile()
    {
        axePlaceholder.SetActive(false);
        GameObject axe = Instantiate(axePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        currentCharHealth -= damage;

        if (currentCharHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        movementInput = GetComponentInParent<PlayerMaster>().movementInput;

        aimInput = GetComponentInParent<PlayerMaster>().aimInput;

        normalAttackInput = GetComponentInParent<PlayerMaster>().normalAttackInput; ;

        specialAttackInput = GetComponentInParent<PlayerMaster>().specialAttackInput;



        //Move Character
        rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, swordAngle));
        //Sword Rotate Pivot Point
        swordRotatePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, swordAngle));
        //Rotate Pivot Point
        rotatePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //If you click attack and you can attack, then attack
        if(normalAttackPauseTime + 0.05 < Time.timeSinceLevelLoad + normalAttackPause)
        {
            weaponHitbox.SetActive(false);
        }
        
        if (normalAttackInput && normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weaponHitbox.SetActive(true);
            //Set time till next attack
            normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
            //This is for switching position of swinging weapons
            if (weaponPos)
            {
                weaponPos = false;
            }
            else
            {
                weaponPos = true;
            }
        }
        if(specialAttackPauseTime < Time.timeSinceLevelLoad)
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
        

    }
}