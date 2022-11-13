using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip bombSound, cannonSound, swordSound, doubleSwordSound, invisibleSound, arrowSound, 
    boxBreakSound;
    static AudioSource audioSrc;
 
    void Start()
    {
        bombSound = GameSounds.Load<AudioClip>("BombSound");
        invisibleSound = GameSounds.Load<AudioClip>("InvisibleSound");
        doubleSwordSound = GameSounds.Load<AudioClip>("Sword_DoubleWhoosh");
        cannonSound = GameSounds.Load<AudioClip>("CannonSound");
        boxBreakSound = GameSounds.Load<AudioClip>("BoxBreakSound");
        arrowSound = GameSounds.Load<AudioClip>("Arrow_Whoosh");
        mainMenuMusic = GameSounds.Load<AudioClip>("MainMenuLoopSong_15sec");
        gameMusic = GameSounds.Load<AudioClip>("MainGameMusic1_2min");
    
    }

    // Update is called once per frame
    void Update()
    {
    }

       public static void PlaySound(string clip)
       {
            switch(clip) {
                case "Bomb":
                audioSrc.PlayOneShot(bombSound);
                break;

                case "Cannon":
                audioSrc.PlayOneShot(cannonSound);
                break;

                case "Invisible":
                audioSrc.PlayOneShot(invisibleSound);
                break;

                case "doubleSword":
                audioSrc.PlayOneShot(doubleSwordSound);
                break;

                case "boxBreak":
                audioSrc.PlayOneShot(boxBreakSound);
                break;

                case "Arrow":
                audioSrc.PlayOneShot(arrowSound);
                break;

            }
       }
    
        public static void PlayLoop(string clip)
        {

                switch(clip) {
                case "GameMusic":
                audioSrc.PlayLoop(gameMusic);
                break;
                }

                switch(clip) {
                case "mainMenuMusic":
                audioSrc.PlayLoop(mainMenuMusic);
                }
        }   
    }
