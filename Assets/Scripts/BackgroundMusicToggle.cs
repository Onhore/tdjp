using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class BackgroundMusicToggle : MonoBehaviour
{
    [SerializeField] AudioSource BackgroundMusic;
    public void EnableMusic() => BackgroundMusic.enabled = !BackgroundMusic.enabled;
    
}
