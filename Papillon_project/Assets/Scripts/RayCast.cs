using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera myCinemachine;
    [SerializeField] GameObject lookingAtGO;
    [SerializeField] int interactableGOLayer;

    // Start is called before the first frame update
    void Start()
    {
        myCinemachine = GetComponentInChildren<CinemachineVirtualCamera>();
        interactableGOLayer = LayerMask.NameToLayer("Interactable");
    }

    private void FixedUpdate()
    {
        StoreGO(myCinemachine.transform.position);
    }

    private void StoreGO(Vector3 _origin)
    {
        // temp variables
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // if the raycast hits anything on the Interactable Layer...
        if (Physics.Raycast(_origin, forward, out hit, Mathf.Infinity, ~interactableGOLayer))
        {
            // debug line
            Debug.DrawRay(transform.position, forward * hit.distance, Color.green);

            // ... store a reference to the game object
            lookingAtGO = hit.transform.gameObject;
            
            // ... if it has the Activate scipt...
            if (lookingAtGO.GetComponent<Activate>() && lookingAtGO != null)
            {
                // ... activate it
                lookingAtGO.GetComponent<Activate>().isActive = true;
            }
        }
        // if it's NOT on the Interactable layer...
        else
        {
            // ... and i WAS looking at an Interactable object...
            if (lookingAtGO)
            {
                // ... if it has the Activate script...
                if (lookingAtGO.GetComponent<Activate>())
                {
                    // ... turn it off before forgetting about it
                    lookingAtGO.GetComponent<Activate>().isActive = false;
                    lookingAtGO = null;
                }
            }
        }
    }
}