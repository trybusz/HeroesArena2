using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)//Any Button Press
        {
            SceneManager.LoadScene(1);// load Start Screen
            //SoundManagerScript.PlayLoop("mainMenuMusic");
        }
    }
}
