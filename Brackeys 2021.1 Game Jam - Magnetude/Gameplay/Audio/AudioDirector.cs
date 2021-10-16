using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioDirector : MonoBehaviour
{
    #region
    private static AudioDirector _instance;

    public static AudioDirector Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [Range(0f, 1f)]
    public float sfxVolume = 1;

    //SFX Banks
    public AudioClip[] hits;
    public AudioClip enemyExplosion;
    public AudioClip[] fanfares;
    public AudioClip[] powerUpSounds;
    public AudioClip[] pauseSounds;
    public AudioClip connectSound;
    public AudioClip disconnectSound;
    public AudioClip damage;

    //SFX Audio Sources
    public AudioSource audioSource;     //Combo Hits
    public AudioSource audioSource2;    //
    public AudioSource audioSource3;    //

    [Range(0f, 1f)]
    public float musicVolume = 1;
    [Range(0f, 1f)]
    private float tempVolume;
    public float fadeSpeed;

    public MusicBank bank;

    public AudioClip introClip;
    public AudioClip loopClip;

    private bool playingIntro;

    public AudioSource musicSource;

    public bool usingTemp;
    public bool isFadingOut;
    public bool isFadingIn;

    //------------------------------------------------------------------------------------------------------------------------
    #region SFX Methods
    public void PlayHit(int h)
    {
        //Play sound based on comboCount
        switch (h)
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
        //if comboCount is too high, just keep playing highest sound
        if (h > 12)
        {
            audioSource.clip = hits[12];
            audioSource.Play();
        }
    }

    public void PlayPowerUpSound(int s) 
    {
        audioSource3.clip = powerUpSounds[s];
        audioSource3.Play();
    }

    public void PlayPause(int s) 
    {
        audioSource3.clip = pauseSounds[s];
        audioSource3.Play();
    }

    public void PlayExplosion() 
    {
        audioSource2.clip = enemyExplosion;
        audioSource2.Play();
    }

    public void PlayConnect() 
    {
        audioSource2.clip = connectSound;
        audioSource2.Play();
    }

    public void PlayDisconnect()
    {
        audioSource2.clip = disconnectSound;
        audioSource2.Play();
    }

    public void PlayDamage()
    {
        audioSource3.clip = damage;
        audioSource3.Play();
    }
    #endregion 

    //------------------------------------------------------------------------------------------------------------------------
    #region Music Methods
    public void FadeOut() 
    {
        tempVolume = musicVolume;
        usingTemp = true;
        isFadingOut = true;
    }

    public void HalveVolume()
    {
        usingTemp = true;
        tempVolume = musicVolume / 2;
    }
    public void ResetVolume()
    {
        usingTemp = false;
        tempVolume = musicVolume;
    }

    private void UseBank() 
    {
        loopClip = bank.loop;
        if (bank.hasIntro) 
        {
            introClip = bank.intro;
            PlayIntro();
            Debug.Log("Finished applying bank");
        }
        else 
        {
            introClip = null;
            musicSource.clip = loopClip;
            musicSource.Play();
            musicSource.loop = true;
        }

        if (musicVolume > 0) 
        {
            isFadingIn = true;
        }
    }

    private void PlayIntro() 
    {
        playingIntro = true;
        musicSource.clip = introClip;
        musicSource.Play();
        musicSource.loop = false;
    }
    #endregion

    //------------------------------------------------------------------------------------------------------------------------
    #region Misc Methods
    private void Update()
    {
        //Go into loop
        if (playingIntro) 
        {
            if (!musicSource.isPlaying) 
            {
                playingIntro = false;
                musicSource.clip = loopClip;
                musicSource.Play();
                musicSource.loop = true;
            }
        }

        //Check if initial Scene
        if (loopClip == null) 
        {
            UseBank();
            //loopClip = bank.loop;

            //if (bank.hasIntro)
            //{
            //    introClip = bank.intro;

            //    //TEMP
            //    musicSource.clip = introClip;
            //    musicSource.Play();
            //}
            //else 
            //{
            //    musicSource.clip = loopClip;
            //    musicSource.Play();
            //}
        }
        
        //Volumes
        if (usingTemp)
        {
            musicSource.volume = tempVolume;
        }
        else 
        {
            musicSource.volume = musicVolume;
        }

        audioSource.volume = sfxVolume;
        audioSource2.volume = sfxVolume;
        audioSource3.volume = sfxVolume;

        //Fading Out NEEDS WORK!!!
        if (isFadingOut && tempVolume > 0)
        {
            //tempVolume -= fadeSpeed * Time.fixedDeltaTime;
            tempVolume -= fadeSpeed;
        }
        else if (isFadingOut && tempVolume <= 0)
        {
            tempVolume = 0;
            isFadingOut = false;
            UseBank();
        }

        //Fading In NEEDS WORK!!!
        if (isFadingIn && tempVolume < musicVolume)
        {
            //tempVolume += fadeSpeed * Time.fixedDeltaTime;
            tempVolume += fadeSpeed;
        }
        else if (isFadingIn && tempVolume >= musicVolume) 
        {
            tempVolume = musicVolume;
            isFadingIn = false;
            usingTemp = false;
        }
    }
    #endregion
}
