using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuControl : MonoBehaviour
{
    //Declare Variables
    public GameObject SelPlay, SelOptions, SelCredits, SelQuit;
    public int MenuSelection = 1; //Used to rotate selection
    //A Button
    private bool SelPress = false;
    //B Button
    private bool BackPress = false;
    private bool upPress = false;
    private bool downPress = false;


    public void OnDpadDown(InputAction.CallbackContext context)
    {
        if (context.performed) { downPress = true; }
            
        else if (context.canceled) { downPress = false; }
            /* released */
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

    // Update is called once per frame
    void Update()
    {
        
        //Use new input system here
        //Cycles through the possible numbers
        if (upPress && MenuSelection != 1)
        {
            MenuSelection -= 1;
        }
        if (downPress && MenuSelection != 4)
        {
            MenuSelection += 1;
        }

        //Display Selection based on number
        if(MenuSelection == 1)
        {
            SelPlay.SetActive(true);
            SelOptions.SetActive(false);
            SelCredits.SetActive(false);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 2)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(true);
            SelCredits.SetActive(false);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 3)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(false);
            SelCredits.SetActive(true);
            SelQuit.SetActive(false);
        }
        if (MenuSelection == 4)
        {
            SelPlay.SetActive(false);
            SelOptions.SetActive(false);
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
            SceneManager.LoadScene(4); //Load Mode Scene
        }
        if (SelPress && MenuSelection == 2)
        {
            SceneManager.LoadScene(3); //Load Options Scene (3)
        }
        if (SelPress && MenuSelection == 3)
        {
            SceneManager.LoadScene(2); //Load Credits Scene (3)
        }
        if (SelPress && MenuSelection == 4)
        {
            Application.Quit();//Exit Game
        }
    }
}
