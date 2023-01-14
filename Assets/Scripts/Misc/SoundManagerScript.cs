using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource Source;
    GameObject[] gameObjects;

    private void Start()
    {

            Source.loop = true;
            Source.Play();
    }

}



    // Update is called once per frame
  
