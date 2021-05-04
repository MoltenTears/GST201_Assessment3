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
    }

    // Start is called before the first frame update
    void Start()
    {
        basePriority = pictureCamera.Priority;
        playerSpeed = myPlayerMovement.speed;
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
            focusOnPicture = false;
        }
    }

    private void CameraSwitcher()
    {
        if (focusOnPicture)
        {
            // get a step speed
            float step = playerSpeed * Time.deltaTime;

            Vector3 tempViewingTrans = new Vector3(viewingPosition.transform.position.x, myPlayerMovement.transform.position.y, viewingPosition.transform.position.z);

            myPlayerMovement.transform.position = Vector3.MoveTowards(myPlayerMovement.transform.position, tempViewingTrans, step);
            
            // if the player is pretty much on the spot
            if (Vector3.Distance(myPlayerMovement.transform.position, tempViewingTrans) < 0.001f)
            {
                myPlayerMovement.speed = 0.0f;

                // vCamFOVOriginal = mainCamera.fieldOfView;
                // mainCamera.fieldOfView += 40f;
            }
            
            
            //// TODO make 2D camera
            // StartCoroutine(CamBlendReleaseCursor());
            //crosshairCanvas.gameObject.SetActive(false);
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = true;
            //HighPriority();
        }
        else
        {
            myPlayerMovement.speed = playerSpeed;
            // mainCamera.fieldOfView = vCamFOVOriginal;

            //// TODO leave as 3D camera
            //BasePriority();
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //crosshairCanvas.gameObject.SetActive(true);
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
