using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource lightSwitch;
    [SerializeField] public AudioSource backgroundMusic;
    [SerializeField] public AudioSource buttonClick;
    [SerializeField] public AudioSource achievementSFX;
    [SerializeField] public AudioSource doorOpenSFX;
    [SerializeField] public AudioSource chestOpenSFX;


    // Singleton
    public static AudioManager AMInstance { get; private set; }

    private void Awake()
    {
        if (AMInstance == null)
        {
            AMInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusic != null) backgroundMusic.Play();
    }

    public void PictureLightOn()
    {
       if (lightSwitch != null) lightSwitch.Play();
    }

    public void ButtonClick()
    {
        if (buttonClick != null) buttonClick.Play();
    }

    public void AchivementSFX()
    {
        if (achievementSFX != null) achievementSFX.Play();
    }

    public void DoorOpenSFX()
    {
        if (doorOpenSFX != null) doorOpenSFX.Play();
    }

    public void ChestOpenSFX()
    {
        if (chestOpenSFX != null) chestOpenSFX.Play();
    }

}
