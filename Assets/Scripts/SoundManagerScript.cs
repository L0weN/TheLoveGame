using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, runSound, attackSound, doubleAttackSound, slideSound;
    static AudioSource audioSrc;
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("Jump");
        runSound = Resources.Load<AudioClip>("Run");
        attackSound = Resources.Load<AudioClip>("Attack");
        doubleAttackSound = Resources.Load<AudioClip>("DoubleAttack");
        slideSound = Resources.Load<AudioClip>("Slide");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                if (!audioSrc.isPlaying)
                {
                    audioSrc.PlayOneShot(jumpSound);
                }
                break;
            case "run":
                if (!audioSrc.isPlaying)
                {
                    audioSrc.PlayOneShot(runSound);
                }
                break;
            case "attack":
                if (!audioSrc.isPlaying)
                {
                    audioSrc.PlayOneShot(attackSound);
                }
                break;
            case "doubleattack":
                if (!audioSrc.isPlaying)
                {
                    audioSrc.PlayOneShot(doubleAttackSound);
                }
                break;
            case "slide":
                if (!audioSrc.isPlaying)
                {
                    audioSrc.PlayOneShot(slideSound);
                }
                break;
        }
    }
    public static void StopSound()
    {
        audioSrc.Stop();
    }
}