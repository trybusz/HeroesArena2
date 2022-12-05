using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuControl : MonoBehaviour
{
    //Declare Variables
    public GameObject SelPlay, SelOptions, SelCredits, SelQuit, SelControls;
    public int MenuSelection = 1; //Used to rotate selection
    //A Button
    private bool SelPress = false;
    //B Button
    private bool BackPress = false;
    private bool upPress = false;
    private bool downPress = false;
    bool dpadVerticalBlocked = false;
    /*
    public void OnDpadDown(InputAction.CallbackContext context)
    {
        if (context.performed) { downPress = true; }
            
        else if (context.canceled) { downPress = false; }
            // released
    }
    public void OnDpadUp(InputAction.CallbackContext context)
    {
        upPress = context.performed;
    }
    public void OnAButton(InputAction.CallbackContext context)
    {
        SelPress = context.performed;
    }
    public void OnBButton(InputAction.CallbackContext context)
    {
        BackPress = context.performed;
    }
    */
    // Update is called once per frame
    void Update()
    {
        //A button
        SelPress = Input.GetButtonDown("Submit");
        //B Button
        BackPress = Input.GetButtonDown("Cancel");


        //If they hit full dpad and its allowed, do stuff
        if ((Input.GetAxis("Vertical") > 0.75f) && (dpadVerticalBlocked == false))
        {

            //Do your button down stuff here
            if (MenuSelection != 5)
            {
                MenuSelection += 1;
            }

            dpadVerticalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Vertical") == 0) { dpadVerticalBlocked = false; }
        }

        if ((Input.GetAxis("Vertical") < -0.75f ) && (dpadVerticalBlocked == false))
        {

            //Do your button down stuff here
            if(MenuSelection != 1)
            {
                MenuSelection -= 1;
            }

            dpadVerticalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Vertical") == 0) { dpadVerticalBlocked = false; }
        }
        /*
        upPress = false;
        downPress = false;
        //Use new input system here
        //Cycles through the possible numbers
        if (upPress && MenuSelection != 1)
        {
            
        }
        if (downPress && MenuSelection != 4)
        {
            
        }
        */
        //Display Selection based on number
        if(MenuSelection == 1)
        {
            SelPlay.SetActive(true);
            SelOptions.SetActive(false);
            SelControls.SetActive(false);
            SelCredits.SetActive(false);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 2)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(true);
            SelControls.SetActive(false);
            SelCredits.SetActive(false);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 3)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(false);
            SelControls.SetActive(true);
            SelCredits.SetActive(false);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 4)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(false);
            SelControls.SetActive(false);
            SelCredits.SetActive(true);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 5)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(false);
            SelControls.SetActive(false);
            SelCredits.SetActive(false);
            SelQuit.SetActive(true);
        }

        //Go back to Start Screen
        if (BackPress)
        {
            SceneManager.LoadScene(0);
        }
        //Go to new screens
        if (SelPress && MenuSelection == 1)
        {
            SceneManager.LoadScene(7); //Load Scene Select, skip mode scene for now
        }
        if (SelPress && MenuSelection == 2)
        {
            SceneManager.LoadScene(10); //Load Options Scene (3)
        }
        if (SelPress && MenuSelection == 3)
        {
            SceneManager.LoadScene(11); //Load Credits Scene (3)
        }
        if (SelPress && MenuSelection == 4)
        {
            SceneManager.LoadScene(2); //Load Credits Scene (3)
        }
        if (SelPress && MenuSelection == 5)
        {
            Application.Quit(); //Exit Game
        }
    }
}
