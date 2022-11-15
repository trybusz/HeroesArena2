using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMaster : MonoBehaviour
{
    public bool char1isDeadFlag = false;
    public bool char2isDeadFlag = false;
    public bool char3isDeadFlag = false;


    public GameObject healthBar;
    CharacterParent cp;
    public Vector3 spawnLocation;

    public bool ready = false;
    public GameObject charPrefab1;
    //bool char1selected = true;
    public GameObject charPrefab2;
    //bool char2selected = false;
    public GameObject charPrefab3;
    //bool char3selected = false;

    public bool AButtonInput { get; private set; }
    public bool BButtonInput { get; private set; }
    public bool XButtonInput { get; private set; }
    public bool YButtonInput { get; private set; }

    private GameObject activePrefab;
    public Transform playerStart;
    public Vector2 movementInput;
    public Vector2 aimInput;
    public bool normalAttackInput;
    public bool specialAttackInput;

    public void InitializePlayer(PlayerConfiguration config)
    {
        charPrefab1 = config.charPrefab1;
        charPrefab2 = config.charPrefab2;
        charPrefab3 = config.charPrefab3;
        //Debug.Log("Player Master: " + GetComponent<PlayerInput>().user);
    }

    // Start is called before the first frame update
    private void Start()
    {
        

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
        if (!charPrefab2.GetComponent<CharacterParent>().isDead)
        {
            activePrefab = charPrefab2;
            if(SceneManager.GetActiveScene().buildIndex == 5)
            {
                charPrefab2.GetComponent<CharacterParent>().TakeStun(1.0f);
            }
            
        }
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        BButtonInput = context.performed;
        if (!charPrefab3.GetComponent<CharacterParent>().isDead)
        {
            activePrefab = charPrefab3;
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                charPrefab3.GetComponent<CharacterParent>().TakeStun(1.0f);
            }
        }
    }
    public void OnXButton(InputAction.CallbackContext context)
    {
        XButtonInput = context.performed;
        if (!charPrefab1.GetComponent<CharacterParent>().isDead)
        {
            activePrefab = charPrefab1;
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                charPrefab1.GetComponent<CharacterParent>().TakeStun(1.0f);
            }
        }
    }
    public void OnYButton(InputAction.CallbackContext context)
    {
        YButtonInput = context.performed;
    }

    // Update is called once per frame
    void Update()
    {
        //add for later scenes 
        if (SceneManager.GetActiveScene().buildIndex == 5 && !ready)
        {
            if (GetComponent<PlayerInput>().playerIndex < 2)
            {
                spawnLocation = new Vector3(-18.0f, 1.0f, 0.0f);
            }
            else
            {
                spawnLocation = new Vector3(18.0f, 1.0f, 0.0f);
            }
            charPrefab1 = Instantiate(charPrefab1, spawnLocation, Quaternion.identity, this.transform);
            charPrefab2 = Instantiate(charPrefab2, spawnLocation, Quaternion.identity, this.transform);
            charPrefab3 = Instantiate(charPrefab3, spawnLocation, Quaternion.identity, this.transform);
            if (GetComponent<PlayerInput>().playerIndex == 0) { healthBar = GameObject.Find("Canvas/HealthBar1"); }
            else if (GetComponent<PlayerInput>().playerIndex == 1) { healthBar = GameObject.Find("Canvas/HealthBar2"); }
            else if (GetComponent<PlayerInput>().playerIndex == 2) { healthBar = GameObject.Find("Canvas/HealthBar3"); }
            else if (GetComponent<PlayerInput>().playerIndex == 3) { healthBar = GameObject.Find("Canvas/HealthBar4"); }


            activePrefab = charPrefab1;
            activePrefab.SetActive(true);
            charPrefab2.SetActive(false);
            charPrefab3.SetActive(false);
            ready = true;
        }

        if (ready)
        {
            if (charPrefab1.GetComponent<CharacterParent>().isDead && char1isDeadFlag == false)
            {
                char1isDeadFlag = true;
                if (!charPrefab2.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab2;
                    activePrefab.transform.position = spawnLocation;
                }
                else if (!charPrefab3.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab3;
                    activePrefab.transform.position = spawnLocation;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
                
            }
            if (charPrefab2.GetComponent<CharacterParent>().isDead && char2isDeadFlag == false)
            {
                char2isDeadFlag = true;
                if (!charPrefab1.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab1;
                    activePrefab.transform.position = spawnLocation;
                }
                else if (!charPrefab3.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab3;
                    activePrefab.transform.position = spawnLocation;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }

            }
            if (charPrefab3.GetComponent<CharacterParent>().isDead && char3isDeadFlag == false)
            {
                char3isDeadFlag = true;
                if (!charPrefab1.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab1;
                    activePrefab.transform.position = spawnLocation;
                }
                else if (!charPrefab2.GetComponent<CharacterParent>().isDead)
                {
                    activePrefab = charPrefab2;
                    activePrefab.transform.position = spawnLocation;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }

            }

            if (charPrefab1 == activePrefab)
            {
                charPrefab3.SetActive(false);
                charPrefab2.SetActive(false);
                cp = charPrefab1.GetComponent<CharacterParent>();
            }
            else if (charPrefab2 == activePrefab)
            {
                charPrefab1.SetActive(false);
                charPrefab3.SetActive(false);
                cp = charPrefab2.GetComponent<CharacterParent>();
            }
            else if (charPrefab3 == activePrefab)
            {
                charPrefab1.SetActive(false);
                charPrefab2.SetActive(false);
                cp = charPrefab3.GetComponent<CharacterParent>();
            }
            activePrefab.SetActive(true);
            charPrefab1.transform.position = activePrefab.transform.position;
            charPrefab2.transform.position = activePrefab.transform.position;
            charPrefab3.transform.position = activePrefab.transform.position;

            healthBar.GetComponent<HealthBarScript1>().SetHealth(cp.GetHealth());
            healthBar.GetComponent<HealthBarScript1>().SetPrimary(cp.GetPrimary());
            healthBar.GetComponent<HealthBarScript1>().SetSpecial(cp.GetSpecial());

            


            //charPrefab1.GetComponent<PlayerInput>().;
        }
    }
}
