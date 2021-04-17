using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField] public bool isAcive = false;

    private void OnTriggerEnter(Collider raycast)
    {
        Debug.Log($"Player is looking at {gameObject.name}");
    }
}
