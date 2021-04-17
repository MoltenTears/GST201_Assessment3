using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField] GameObject lookingAtGO;
    [SerializeField] int interactableGOLayer;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        interactableGOLayer = LayerMask.NameToLayer("Interactable");
    }

    private void FixedUpdate()
    {
        StoreGO(myCamera.transform.position);
    }

    private void StoreGO(Vector3 _origin)
    {
        // temp variables
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // if the raycast hits anything on the Interactable Layer...
        if (Physics.Raycast(_origin, forward, out hit, Mathf.Infinity, ~interactableGOLayer))
        {
            // ... store a reference to the game object
            lookingAtGO = hit.transform.gameObject;
            
            // ... if it has the Activate scipt...
            if (lookingAtGO.GetComponent<Activate>() && lookingAtGO != null)
            {
                // ... activate it
                lookingAtGO.GetComponent<Activate>().isAcive = true;
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
                    lookingAtGO.GetComponent<Activate>().isAcive = false;
                    lookingAtGO = null;
                }
            }
        }
    }
}
