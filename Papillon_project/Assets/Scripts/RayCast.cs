using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera myCinemachine;
    [SerializeField] GameObject lookingAtGO;
    [SerializeField] int interactableGOLayer;
    [SerializeField] private RaycastHit raycastHit;
    // Start is called before the first frame update
    void Start()
    {
        myCinemachine = GetComponentInChildren<CinemachineVirtualCamera>();
        interactableGOLayer = LayerMask.NameToLayer("Interactable");
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        StoreGO(myCinemachine.transform.position); 
        ActivateObject();
    }

    private void ActivateObject()
    {
        if (lookingAtGO != null && lookingAtGO.GetComponent<Activate>())
        {
            lookingAtGO.GetComponent<Activate>().isActive = true;
        }
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
            Debug.Log($"Player is looking at {hit.transform.gameObject.name}.");

            // ... if  the hit object has the Activate scipt and a lookingAtGO is already stored...
            if (hit.transform.gameObject.GetComponent<Activate>() && lookingAtGO != null)
            {
                // if it's NOT the same object and the previous object has the Activate script...
                if (lookingAtGO.transform.name != hit.transform.name)
                { 
                    // turn off the previous object's status
                    if (lookingAtGO.GetComponent<Activate>())
                    {
                        Debug.Log($"{lookingAtGO.name} no longer being looked at.");
                        lookingAtGO.GetComponent<Activate>().isActive = false;
                    }

                    // ...  store a new version
                    lookingAtGO = hit.transform.gameObject;
                }
            }
            else
            {
                // ... store a reference to the object
                lookingAtGO = hit.transform.gameObject;
            }
        }

        // if it's NOT on the Interactable layer...
        else
        {
            // ... and i WAS looking at an Interactable object...
            if (lookingAtGO != null)
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
