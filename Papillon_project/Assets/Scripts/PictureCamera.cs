using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PictureCamera : MonoBehaviour
{
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
            HighPriority();
            pictureCamera.m_Lens.OrthographicSize = orthoSize;
            // mainCamera.orthographic = true;
        }
        else
        {
            // TODO leave as 3D camera
            BasePriority();
            // mainCamera.orthographic = false;
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
