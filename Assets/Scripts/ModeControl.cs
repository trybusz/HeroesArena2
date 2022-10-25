using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ModeControl : MonoBehaviour
{
    //Declare Variables
    public GameObject Sel2v2, Sel1v1, SelFFA, SelBack;
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
        if (MenuSelection == 1)
        {
            Sel2v2.SetActive(true);
            Sel1v1.SetActive(false);
            SelFFA.SetActive(false);
            SelBack.SetActive(false);
        }
        if (MenuSelection == 2)
        {
            Sel2v2.SetActive(false);
            Sel1v1.SetActive(true);
            SelFFA.SetActive(false);
            SelBack.SetActive(false);
        }
        if (MenuSelection == 3)
        {
            Sel2v2.SetActive(false);
            Sel1v1.SetActive(false);
            SelFFA.SetActive(true);
            SelBack.SetActive(false);
        }
        if (MenuSelection == 4)
        {
            Sel2v2.SetActive(false);
            Sel1v1.SetActive(false);
            SelFFA.SetActive(false);
            SelBack.SetActive(true);
        }

        //Go back to Start Screen
        if (BackPress)
        {
            SceneManager.LoadScene(0);
        }
        //Go to new screens
        if (SelPress && MenuSelection == 1)
        {
            //SceneManager.LoadScene(x); //Load 2v2
        }
        if (SelPress && MenuSelection == 2)
        {
            //SceneManager.LoadScene(x); //Load 1v1
        }
        if (SelPress && MenuSelection == 3)
        {
            //SceneManager.LoadScene(x); //Load FFA
        }
        if (SelPress && MenuSelection == 4)
        {
            SceneManager.LoadScene(1);//Back to main menu
        }
    }
}
