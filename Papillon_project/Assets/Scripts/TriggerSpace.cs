using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpace : MonoBehaviour
{
    [SerializeField] private GameObject collisionObject;

    private GameManager myGameManager;

    private void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionObject = collision.gameObject;
                        
            if (!myGameManager.triedDoor)
            {
                myGameManager.triedDoor = true; // TODO convert to Event
                
            }                   
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionObject = null;
        }
    }
}
