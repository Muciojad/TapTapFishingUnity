using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages music and sounds.
/// </summary>
public class GlobalSoundSystem : MonoBehaviour
{
    public static GlobalSoundSystem instance;

    [SerializeField] private AudioSource backgroundMusic, sounds;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite imageMuted, imageUnmuted;


    private void Awake()
    {
        instance = this;
    }

    public void Mute()
    {
        if(backgroundMusic.volume == 0.0)
        {
            backgroundMusic.volume = 1f;
            sounds.volume = 1f;
            soundImage.sprite = imageUnmuted;
        }
        else
        {
            backgroundMusic.volume = 0f;
            sounds.volume = 0f;
            soundImage.sprite = imageMuted;
        }
    }

    public void PlaySound()
    {
        sounds.Play();
    }
}
