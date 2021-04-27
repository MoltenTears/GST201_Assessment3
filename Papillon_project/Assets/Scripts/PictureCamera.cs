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
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int basePriority;
    [SerializeField] private int highPriority;
    [SerializeField] public bool focusOnPicture;

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
    }

    private void Update()
    {
        EscapePicture();
        CameraSwitcher();

    }
    private void EscapePicture()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            focusOnPicture = false;
        }
    }

    private void CameraSwitcher()
    {
        if (focusOnPicture)
        {
            // TODO make 2D camera
            crosshairCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            HighPriority();
        }
        else
        {
            // TODO leave as 3D camera
            BasePriority();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            crosshairCanvas.gameObject.SetActive(true);
        }
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
