using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DescriptionsScript : MonoBehaviour
{
    public TextMeshProUGUI[] descriptions;
    public TextMeshProUGUI description;
    private bool dpadHorizontalBlocked;
    private int selectedCharacter;
    public bool BackPress = false;

    //public int MenuSelection { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BackPress = Input.GetButtonDown("Cancel");
        //If they hit full dpad and its allowed, do stuff
        if ((Input.GetAxis("Horizontal") > 0.75f) && (dpadHorizontalBlocked == false))
        {

            //Do your button down stuff here

            selectedCharacter = (selectedCharacter + 1) % descriptions.Length;
            description.SetText(descriptions[selectedCharacter].text);

            //MenuSelection += 1;
            dpadHorizontalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Horizontal") == 0) { dpadHorizontalBlocked = false; }
        }

        if ((Input.GetAxis("Horizontal") < -0.75f) && (dpadHorizontalBlocked == false))
        {

            //Do your button down stuff here

            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter += descriptions.Length;
            }
            description.SetText(descriptions[selectedCharacter].text);

            //MenuSelection -= 1;
            dpadHorizontalBlocked = true; //Disable it

        }
        else
        {
            //if they release, allow them to hit it again.
            if (Input.GetAxis("Horizontal") == 0) { dpadHorizontalBlocked = false; }
        }

        if (BackPress)
        {
            SceneManager.LoadScene(1);
        }
    }
}
