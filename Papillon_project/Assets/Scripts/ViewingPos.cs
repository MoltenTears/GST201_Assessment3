using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingPos : MonoBehaviour
{
    [SerializeField] private PictureCamera myPictureCamera;

    // Start is called before the first frame update
    void Start()
    {
        myPictureCamera = FindObjectOfType<PictureCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} entered ViewingPos.");
        // if the other is the player
        if (other.GetComponent<PlayerMovement>() && myPictureCamera.focusOnPicture)
        {
            Debug.Log("Player entered the ViewingPos Trigger while trying to view the Picture.");
        }
    }
}
