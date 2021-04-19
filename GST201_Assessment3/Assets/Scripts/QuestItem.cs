using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    [Header("Quest Details")]
    [SerializeField] public QuestEnums.QuestName myQuestName;
    [SerializeField] public QuestEnums.QuestStatus myQuestStatus;
    [SerializeField] public QuestEnums.QuestName nextQuestName;

    [Header("Debugging Colours")]
    [SerializeField] private MeshRenderer myMeshRenderer;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material completedMaterial;


    private void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        nextQuestName = myQuestName + 1;
    }

    private void Update()
    {
        // Debug.Log($"questItem status: {myQuestStatus}.");
        ChangeColour();
    }

    private void ChangeColour()
    {
        switch (myQuestStatus)
        {
            case QuestEnums.QuestStatus.INACTIVE:
                {
                    myMeshRenderer.material = inactiveMaterial;
                    break;
                }
            case QuestEnums.QuestStatus.ACTIVE:
                {
                    myMeshRenderer.material = activeMaterial;
                    break;
                }
            case QuestEnums.QuestStatus.COMPLETED:
                {
                    myMeshRenderer.material = completedMaterial;
                    break;
                }
            case QuestEnums.QuestStatus.NONE:
                {
                    myMeshRenderer.material = inactiveMaterial;
                    break;
                }
            default:
                {
                    Debug.LogError("Colour switcher has defaulted, check QuestItem.cs");
                    break;
                }
        }
    }


    public void CompleteQuest()
    {
        Debug.Break();
        Debug.Log("CompleteQuest() called.");
        Debug.Log($"Quest status is now: {myQuestStatus}");
        myQuestStatus = QuestEnums.QuestStatus.COMPLETED;
        Debug.Log($"Quest status is now: {myQuestStatus}");
        Debug.Break();
    }

    public void ActivateQuest()
    {
        if (myQuestStatus == QuestEnums.QuestStatus.INACTIVE)
        {
            Debug.Log($"Quest {myQuestName} activated.");
            myQuestStatus = QuestEnums.QuestStatus.ACTIVE;
        }
        else
        {
            Debug.LogError("Quest already active, check QuestItem.cs.");
        }
    }


}
