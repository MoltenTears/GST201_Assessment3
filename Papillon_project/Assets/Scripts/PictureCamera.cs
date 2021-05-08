using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PictureCamera : MonoBehaviour
{
    [SerializeField] private MouseLookAt myMouseLookAt;
    [SerializeField] private PlayerMovement myPlayerMovement;
    [SerializeField] private Canvas crosshairCanvas;
    [SerializeField] private CinemachineVirtualCamera pictureCamera;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int basePriority;
    [SerializeField] private int highPriority;
    [SerializeField] public bool focusOnPicture;
    [SerializeField] public bool isTransitioning;
    [SerializeField] public float playerSpeed;
    [SerializeField] private GameObject viewingPosition;
    [SerializeField] private float vCamFOVOriginal;
    [SerializeField] private Vector3 posPriorToFocusing;
    [SerializeField] private bool focusPositionTriggered;
    [SerializeField] private float stepOutOfPaintingX;
    [SerializeField] private bool stepOutTriggered;
    [SerializeField] private float FOVClose;
    [SerializeField] private float FOVOriginal;
    [SerializeField] [Range(0,1)] private float FOVSpeed;

    [SerializeField] private Vector3 tempViewingTrans;

    [Header("Ortho Stats")]
    [SerializeField] private float orthoSize;


    private void Awake()
    {
        if(FindObjectOfType<Camera>())
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        else
        {
            Debug.LogError("Cannot find standard Camera in scene.");
        }

        if (GetComponent<CinemachineVirtualCamera>())
        {
            pictureCamera = GetComponent<CinemachineVirtualCamera>();
        }
        else
        {
            Debug.LogError("Cannot find CinemachineVirtualCamera for PictureCamera.cs");
        }

        if (FindObjectOfType<PlayerMovement>())
        {
            myPlayerMovement = FindObjectOfType<PlayerMovement>();
        }
        else
        {
            Debug.LogError("Cannot find PlayerMovement in scene.");

        }

        myMouseLookAt = FindObjectOfType<MouseLookAt>();
        Cursor.lockState = CursorLockMode.Locked;

        playerCamera = myPlayerMovement.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        basePriority = pictureCamera.Priority;
        playerSpeed = myPlayerMovement.speed;
        FOVOriginal = playerCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        EscapePicture();
        CameraSwitcher();

    }
    private void EscapePicture()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (focusOnPicture)
            {
                focusOnPicture = false;
            }
        }
    }

    private void CameraSwitcher()
    {
        if (focusOnPicture)
        {
            myPlayerMovement.enabled = false;
            
            // get a step speed
            float step = playerSpeed * Time.deltaTime;

            // store a temporary position in front of the painting
            tempViewingTrans = new Vector3(viewingPosition.transform.position.x, myPlayerMovement.transform.position.y, viewingPosition.transform.position.z);

            // if there is nothing stored in the Vector3...
            if (posPriorToFocusing == Vector3.zero)
            {
                // Debug.Log("Getting previous player position.");
                // get the player's position prior to focusing on the painting
                posPriorToFocusing = myPlayerMovement.transform.position;
            }

            // take a step towards the painting
            myPlayerMovement.transform.position = Vector3.MoveTowards(myPlayerMovement.transform.position, tempViewingTrans, step);

            // set the camera
            playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, FOVClose, (playerSpeed * Time.deltaTime) * FOVSpeed / 2);

            // if the player is pretty much on the spot
            if (Vector3.Distance(myPlayerMovement.transform.position, tempViewingTrans) <= 0.01f)
            {
                transform.position += Vector3.zero;
            }
        }
        else
        {
            myPlayerMovement.enabled = true;

            // if there was a previous position stored...
            if (posPriorToFocusing != Vector3.zero)
            {
                // ... get a step speed
                float step = playerSpeed * Time.deltaTime;

                // if the value hasn't already been set...
                if (!stepOutTriggered)
                {
                    // stop re-checking it
                    stepOutTriggered = true;

                    // get the new location to step to
                    stepOutOfPaintingX = myPlayerMovement.transform.position.x - 0.5f;
                }
                // get a Vector3 to move to
                Vector3 tempReturnTrans = new Vector3(stepOutOfPaintingX, myPlayerMovement.transform.position.y, myPlayerMovement.transform.position.z);

                // if the movement is not yet complete...
                if (Vector3.Distance(myPlayerMovement.transform.position, tempReturnTrans) > 0.01f)
                {
                    // ... move back to the previous position priot to focusing
                    myPlayerMovement.transform.position = Vector3.MoveTowards(myPlayerMovement.transform.position, tempReturnTrans, step);
                }
                // once there...
                else
                {
                    // reset for next entry
                    stepOutTriggered = false;

                    // reset the Vector3
                    posPriorToFocusing = Vector3.zero;
                }
            }

            // reset the camera
            playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, FOVOriginal, (playerSpeed * Time.deltaTime) * FOVSpeed);

        }
    }

    private IEnumerator CamBlendReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock during transition
        Debug.Log("Cursor locked...");
        yield return new WaitForSeconds(3);
        Cursor.lockState = CursorLockMode.None; // unlock after delay
        Debug.Log("...Cursor released!");

    }

    public void IncreasePriority(int _value)
    {
        pictureCamera.Priority = _value;
    }

    public void DecreasePriority(int _value)
    {
        pictureCamera.Priority = _value;
    }

    public void BasePriority()
    {
        pictureCamera.Priority = basePriority;
    }

    public void HighPriority()
    {
        pictureCamera.Priority = highPriority;
    }
}
