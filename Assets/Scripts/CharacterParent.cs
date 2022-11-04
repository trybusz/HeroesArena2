using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterParent : MonoBehaviour
{

    public int health = 150;
    // Start is called before the first frame update
   
    /*Rotating Weapon
    public GameObject weapon;
    public GameObject weaponHitbox;
    //Point to Rotate stuff around
    public GameObject rotatePoint;
    //Point to Rotate Weapon Around
    //public GameObject swordRotatePoint;
    //Player RigidBody
    private Rigidbody2D rb;
    //Base character speed
    public float charSpeed = 4.0f;
    //Base Character Health
    //public int maxCharHealth = 150;
    //Current Health
    //public int currentCharHealth = 150;
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
    //private float normalAttackPause = 0.5f;
    //Variable to measure cooldown
    private float normalAttackPauseTime = 0.0f;
    //SPECIAL ATTACK
    //Special Attack Speed;
    //public float projectileSpeed = 10.0f;
    //Transform for fire point
    public GameObject firePoint;
    //Axe Prefab
    //public GameObject axePrefab;
    //Special Attack Button
    private bool specialAttackInput = false;
    //How long of a cooldown on normal attack
    private static float specialAttackPause = 4.0f;
    //Variable to measure cooldown on special attack
    private float specialAttackPauseTime = 0.0f;
    //Axe On Character Object
    //public GameObject axePlaceholder;
    //Angle to hold weapon, set by joystick
    float angle = 0.0f;
    //Forward Angle, set by joystick
    //float swordAngle = 0.0f;
    //float lastSwordAngle = 0.0f;
    //Previous angle, to be used as a temp on angle
    float lastAngle = 0.0f;
    //A Button
   
    */

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

