using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBank : MonoBehaviour
{
    public bool hasIntro;

    public AudioClip intro;
    public AudioClip loop;

    private void Start()
    {
        //On scene start this replaces the audio bank so the Audio Director can change the current music
        AudioDirector.Instance.bank = this;
    }
}
