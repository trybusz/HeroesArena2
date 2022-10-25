using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //Declare Variables
    public GameObject SelPlay, SelOptions, SelCredits, SelQuit;
    public int MenuSelection = 1; //Used to rotate selection
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool upPress = Input.GetKeyDown("w");
        bool downPress = Input.GetKeyDown("s");
        bool SelPress = Input.GetKeyDown("j");//like an a button
        bool BackPress = Input.GetKeyDown("k");//like a b button
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
