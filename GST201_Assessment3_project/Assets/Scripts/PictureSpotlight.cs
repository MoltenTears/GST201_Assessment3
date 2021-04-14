using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSpotlight : MonoBehaviour
{
    [SerializeField] private Light pictureSpotlight;
    [SerializeField] private GameManager myGameManager;
    [SerializeField] private AudioSource pictureLight;

    private void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        pictureSpotlight = GetComponent<Light>();
    }

    void Update()
    {
        if (myGameManager.triedDoor && !myGameManager.pictureLightOn)
        {
            myGameManager.pictureLightOn = true;
            pictureSpotlight.enabled = true;
            if (pictureLight != null)
            {
                pictureLight.Play(); // TODO convert to Event
            }
        }
    }
}
