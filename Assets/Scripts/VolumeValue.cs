using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeValue : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    
    public void SetVolume(float vol)
    {
        mixer.SetFloat("MainVolume", vol);
    }
}
