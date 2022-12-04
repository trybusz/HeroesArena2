using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CountdownScript : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    // Start is called before the first frame update
    void Start()
    {
        countdown = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown.SetText((5 - (int)Time.timeSinceLevelLoad).ToString());

        if(5 - Time.timeSinceLevelLoad < 0){
            Destroy(gameObject);
        }
    }
}
