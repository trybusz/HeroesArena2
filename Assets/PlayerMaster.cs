using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMaster : MonoBehaviour
{
    public GameObject cursor;
    public GameObject charPrefab1;
    bool char1selected = true;
    public GameObject charPrefab2;
    bool char2selected = false;
    public GameObject charPrefab3;
    bool char3selected = false;

    public bool AButtonInput { get; private set; }
    public bool BButtonInput { get; private set; }
    public bool XButtonInput { get; private set; }
    public bool YButtonInput { get; private set; }

    private GameObject activePrefab;
    public static Transform player1start;
    public static Transform player2start;
    public static Transform player3start;
    public static Transform player4start;
    public Vector2 movementInput;
    public Vector2 aimInput;
    public bool normalAttackInput;
    public bool specialAttackInput;

    // Start is called before the first frame update
    private void Start()
    {
        charPrefab1 = Instantiate(charPrefab1, new Vector3(10, 0, 0), new Quaternion(0, 0, 0, 0), this.transform);
        charPrefab2 = Instantiate(charPrefab2, new Vector3(10, 0, 0), new Quaternion(0, 0, 0, 0), this.transform);
        charPrefab3 = Instantiate(charPrefab3, new Vector3(10, 0, 0), new Quaternion(0, 0, 0, 0), this.transform);
        
        //default character #1
        activePrefab = charPrefab1;
        activePrefab.SetActive(true);
        charPrefab2.SetActive(false);
        charPrefab3.SetActive(false);
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
        activePrefab = charPrefab2;
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        BButtonInput = context.performed;
        activePrefab = charPrefab3;
    }
    public void OnXButton(InputAction.CallbackContext context)
    {
        XButtonInput = context.performed;
        activePrefab = charPrefab1;
    }
    public void OnYButton(InputAction.CallbackContext context)
    {
        YButtonInput = context.performed;
    }

    // Update is called once per frame
    void Update()
    {

        if (charPrefab1 != activePrefab)
        {
            charPrefab1.SetActive(false);
        }
        if (charPrefab2 != activePrefab)
        {
            charPrefab2.SetActive(false);
        }
        if (charPrefab3 != activePrefab)
        {
            charPrefab3.SetActive(false);
        }
        activePrefab.SetActive(true);
        charPrefab1.transform.position = activePrefab.transform.position;
        charPrefab2.transform.position = activePrefab.transform.position;
        charPrefab3.transform.position = activePrefab.transform.position;

        //charPrefab1.GetComponent<PlayerInput>().;
    }

}
