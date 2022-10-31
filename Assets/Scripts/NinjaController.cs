using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NinjaController : MonoBehaviour
{
    //Rotating Weapon
    public GameObject weapon;
    public GameObject dashHitbox;
    public bool dashing = false;
    //Point to Rotate stuff around
    public GameObject rotatePoint;
    //Player RigidBody
    private Rigidbody2D rb;
    //Base character speed
    public float charSpeed = 4.0f;
    //Modifier for how fast each character is relatively
    public float charSpeedMod = 1.0f;
    //Base Character Health
    public int maxCharHealth = 150;
    //Current Health
    public int currentCharHealth = 150;
    //Movement input
    private Vector2 movementInput = Vector2.zero;
    //Aim Imput
    private Vector2 aimInput = Vector2.zero;
    //NORMAL ATTACK
    //Normal Attack Button
    private bool normalAttackInput = false;
    //How long of a cooldown on normal attack
    private float normalAttackPause = 9.0f;
    //Variable to measure cooldown
    private float normalAttackPauseTime = 0.0f;
    //SPECIAL ATTACK
    //Special Attack Speed;
    public float projectileSpeed = 17.0f;
    //Transform for fire point
    public GameObject firePoint1;
    //Transform for fire point
    public GameObject firePoint2;
    //Transform for fire point
    public GameObject firePoint3;
    //Axe Prefab
    public GameObject starPrefab;
    //Special Attack Button
    private bool specialAttackInput = false;
    //How long of a cooldown on normal attack
    private static float specialAttackPause = 0.3f;
    //Variable to measure cooldown on special attack
    private float specialAttackPauseTime = 0.0f;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //Get Inputs
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        normalAttackInput = context.performed;
    }
    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        specialAttackInput = context.performed;
    }
    public void OnAButton(InputAction.CallbackContext context)
    {
        AButtonInput = context.performed;
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        BButtonInput = context.performed;
    }
    public void OnXButton(InputAction.CallbackContext context)
    {
        XButtonInput = context.performed;
    }
    public void OnYButton(InputAction.CallbackContext context)
    {
        YButtonInput = context.performed;
    }
    void ShootProjectile()
    {
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
    void Update()
    {
        //Move Character
        if (dashing)
        {
            dashHitbox.SetActive(true);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod * 20;
        }
        else
        {
            dashHitbox.SetActive(false);
            rb.velocity = new Vector2(movementInput.x, movementInput.y) * charSpeed * charSpeedMod;
        }
        //Rotate Weapon
        weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate Pivot Point
        rotatePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Rotate True Aim Point
        firePoint1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        firePoint2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 10));
        firePoint3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 10));
        //If you click attack and you can attack, then attack
        if (normalAttackPauseTime + 0.10 > Time.timeSinceLevelLoad + normalAttackPause)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }

        if (normalAttackInput && normalAttackPauseTime < Time.timeSinceLevelLoad)
        {
            //Set time till next attack
            normalAttackPauseTime = Time.timeSinceLevelLoad + normalAttackPause;
        }
        if (specialAttackPauseTime < Time.timeSinceLevelLoad)
        {
            weapon.SetActive(true);
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