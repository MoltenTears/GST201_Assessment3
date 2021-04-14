using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource horror1;
    [SerializeField] public AudioSource horror2;
    [SerializeField] public AudioSource horror3;
    [SerializeField] public AudioSource lightSwitch;

    public void PictureLightOn()
    {
        lightSwitch.Play();
    }
}
