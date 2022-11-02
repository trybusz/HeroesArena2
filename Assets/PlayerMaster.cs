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
    private GameObject activePrefab;
    public static Transform player1start;
    public static Transform player2start;
    public static Transform player3start;
    public static Transform player4start;

    // Start is called before the first frame update
    void Start()
    {
        charPrefab1 = Instantiate(charPrefab1, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        charPrefab1.SetActive(true);
        charPrefab2 = Instantiate(charPrefab2, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        charPrefab2.SetActive(false);
        charPrefab3 = Instantiate(charPrefab3, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        charPrefab3.SetActive(false);
    }

    public void OnXButton(InputAction.CallbackContext context)
    {
        char1selected = context.performed;
        activePrefab = charPrefab1;
    }
    public void OnAButton(InputAction.CallbackContext context)
    {
        char2selected = context.performed;
        activePrefab = charPrefab2;
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        char3selected = context.performed;
        activePrefab = charPrefab3;
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
         * if cursor is over prefab, set prefab1, 2, then 3
         */
        
        if(charPrefab1 != activePrefab)
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

    }

}
