using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLaunch : MonoBehaviour
{
    public float timeOfInst;
    public float airTime;
    public GameObject mineSetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        airTime = 0.5f;
        timeOfInst = Time.timeSinceLevelLoad + airTime;
        
    }


    // Update is called once per frame
    void Update()
    {
        if (timeOfInst < Time.timeSinceLevelLoad)
        {
            Instantiate(mineSetPrefab, this.transform.position, this.transform.rotation);
            //SoundManagerScript.PlaySound("Bomb");
            Destroy(gameObject);
        }
    }
}