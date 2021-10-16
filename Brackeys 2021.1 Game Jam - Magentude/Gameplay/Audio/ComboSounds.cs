using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSounds : MonoBehaviour
{
    public AudioClip[] hits;
    public AudioClip[] fanfares;

    public AudioSource audioSource;

    public void PlayHit(int h) 
    {
        //Determines which sound to play
        switch(h)
        {
            case 0:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 4:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 5:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 6:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 7:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 8:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 9:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 10:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 11:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
            case 12:
                audioSource.clip = hits[h];
                audioSource.Play();
                break;
        }
        //For overload scenarios it repeats the highest sound
        if (h > 12) 
        {
            audioSource.clip = hits[12];
            audioSource.Play();
        }
    }
}
