using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField] public bool isActive = false;
    [HideInInspector] private QuestItem myQuestItem;
    [HideInInspector] private MeshRenderer myMeshRender;
    [HideInInspector] private GameManager myGameManager;
    [HideInInspector] private Material myOriginalMaterial;
    [SerializeField] private PictureCamera myPictureCamera;

    

    private void Start()
    {
        GetReferences();
    }


    private void Update()
    {
        if (myMeshRender != null) myOriginalMaterial = myMeshRender.material;

        HighlightColour();
        Interact();
    }





    private void Interact()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Mouse0))
        {
            // if this is the picture, change cameras
            if (myQuestItem.myQuestName == QuestEnums.QuestName.NOT_STARTED
                || myQuestItem.myQuestName == QuestEnums.QuestName.B
                || myQuestItem.myQuestName == QuestEnums.QuestName.D)
            {
                Debug.Log("activated picture camera!");
                myPictureCamera.focusOnPicture = true;
            }

            if (myQuestItem.myQuestStatus == QuestEnums.QuestStatus.ACTIVE)
            {
                // myQuestItem.CompleteQuest();
                myGameManager.UpdateQuests(myQuestItem);
            }
        }
    }

    public void HighlightColour()
    {
        if (myMeshRender != null)
        {
            if (isActive)
            {
                myMeshRender.material.color = myGameManager.highlightColour;
            }
            else
            {
                myMeshRender.material.color = myOriginalMaterial.color;
            }
        }
    }

    public void GetReferences()
    {
        // Get MeshRenderer
        if (GetComponent<MeshRenderer>())
        {
            myMeshRender = GetComponent<MeshRenderer>();
            myOriginalMaterial = myMeshRender.sharedMaterial;
        }
        else
        {
            // Debug.LogWarning($"Missing MeshRenderer on {gameObject.transform.name}, check Activate.cs.");
        }

        // Get PictureCamera
        if (GetComponentInChildren<PictureCamera>())
        {
            myPictureCamera = GetComponentInChildren<PictureCamera>();
        }

        // Get QuestItem
        if (GetComponent<QuestItem>())
        {
            myQuestItem = GetComponent<QuestItem>();
        }
        else
        {
            Debug.LogWarning($"Missing QuestItem on {gameObject.transform.name}, check Activate.cs.");
        }

        // Get Game Manager
        if (FindObjectOfType<GameManager>())
        {
            myGameManager = FindObjectOfType<GameManager>();
        }
        else
        {
            Debug.LogWarning($"Missing GameManager on {gameObject.transform.name}, check Activate.cs.");
        }
    }

}
