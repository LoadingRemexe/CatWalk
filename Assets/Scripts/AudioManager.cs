using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource SFXAudio;
    [SerializeField] AudioClip[] SFXOptions;
     
    public void PlaySFX(int index)
    {
        if (index > 0 && index < SFXOptions.Length)
        {
            SFXAudio.clip = SFXOptions[index];
            SFXAudio.Play();
        }
    }

    public void PlayRandomMeow()
    {
        PlaySFX(Random.Range(0, 3));
    }
}
