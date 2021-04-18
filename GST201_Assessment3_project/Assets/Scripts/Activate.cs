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

    

    private void Start()
    {
        GetReferences();
    }


    private void Update()
    {
        myOriginalMaterial = myMeshRender.material;

        HighlightColour();
        Interact();
    }

    private void Interact()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (myQuestItem.myQuestStatus == QuestEnums.QuestStatus.ACTIVE)
            {
                // myQuestItem.CompleteQuest();
                myGameManager.UpdateQuests(myQuestItem);
            }
        }
    }

    public void HighlightColour()
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

    private void GetReferences()
    {
        // Get MeshRenderer
        if (GetComponent<MeshRenderer>())
        {
            myMeshRender = GetComponent<MeshRenderer>();
            myOriginalMaterial = myMeshRender.sharedMaterial;
        }
        else
        {
            Debug.LogWarning($"Missing MeshRenderer on {gameObject.transform.name}, check Activate.cs.");
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
