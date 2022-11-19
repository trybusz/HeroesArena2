using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndControl : MonoBehaviour
{
    public int winCase;
    public TextMeshProUGUI winCaseText;
    // Start is called before the first frame update
    void Start()
    {
        //destroys the dont destroy on load object
        Destroy(GameObject.Find("MainLayout"));

        //GameEndScript ges = new GameEndScript();
        winCase = GameEndScript.winCase;

        if(winCase == 0)
        {
            winCaseText.SetText("Blue team won by eliminating the other team");
        }
        if (winCase == 1)
        {
            winCaseText.SetText("Red team won by eliminating the other team");
        }
        if (winCase == 2)
        {
            winCaseText.SetText("Blue team won by capturing the point");
        }
        if (winCase == 3)
        {
            winCaseText.SetText("Red team won by capturing the point");
        }

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
