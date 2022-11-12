using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Control Point?
        //Sound Effects Volume
        //Music Volume
        bool BackPress = Input.GetKeyDown("k");//like a b button
        if (BackPress)
        {
            SceneManager.LoadScene(1); //Load Menu Scene
        }
    }
}
