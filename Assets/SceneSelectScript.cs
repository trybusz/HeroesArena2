using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSelectScript : MonoBehaviour
{
    //map1 default
    public static int selectedStage = 5;
    public SpriteRenderer Map1;
    public SpriteRenderer Map2;

    public int MenuSelection = 1; //Used to rotate selection
    // Start is called before the first frame update
    bool dpadVerticalBlocked = false;
    void Start()
    {
        Map1.enabled = true;
        Map2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        bool SelPress = Input.GetButtonDown("Submit");  //like an a button
        bool BackPress = Input.GetButtonDown("Cancel"); //like a b button

        //If they hit full dpad and its allowed, do stuff
        if ((Input.GetAxis("Horizontal") > 0.75f) && (dpadVerticalBlocked == false))
        {

            //Do your button down stuff here
            MenuSelection += 1;
            dpadVerticalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Horizontal") == 0) { dpadVerticalBlocked = false; }
        }

        if ((Input.GetAxis("Horizontal") < -0.75f) && (dpadVerticalBlocked == false))
        {

            //Do your button down stuff here
            MenuSelection -= 1;
            dpadVerticalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Horizontal") == 0) { dpadVerticalBlocked = false; }
        }


        //Display Selection based on number
        if (MenuSelection == 1)
        {
            Map1.enabled = true;
            Map2.enabled = false;

            if (SelPress)
            {
                selectedStage = 5;
                SceneManager.LoadScene(8); //Load 2v2 char select
            }
        }
        if (MenuSelection == 2)
        {
            Map1.enabled = false;
            Map2.enabled = true;

            if (SelPress)
            {
                selectedStage = 6;
                SceneManager.LoadScene(8); //Load 2v2 char select
            }
        }
        //just for 2 map looping for now
        if (MenuSelection > 2)
        {
            MenuSelection = 1;
        }
        if (MenuSelection < 1)
        {
            MenuSelection = 2;
        }


        //Go back to mode select
        if (BackPress)
        {
            SceneManager.LoadScene(4);
        }
    }
}
