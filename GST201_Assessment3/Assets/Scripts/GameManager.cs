using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    // variables for collectible variables
    [SerializeField] public bool triedDoor = false;
    [SerializeField] public bool pictureLightOn = false;
    [SerializeField] public Color highlightColour;

    [Header("Quest Details")]
    [SerializeField] public List<QuestItem> questList;
    [HideInInspector] private QuestItem[] myQuestItemArr;
    [SerializeField] public QuestEnums.QuestName currentQuest;

    private void Start()
    {
        GetQuests();
    }

    private void Update()
    {
        
    }

    private void GetQuests()
    {
        myQuestItemArr = FindObjectsOfType<QuestItem>();
        foreach (QuestItem questItem in myQuestItemArr)
        {
            questList.Add(questItem);
        }
    }

    public void UpdateQuests(QuestItem _questItem)
    {
        QuestEnums.QuestName tempQuestName = _questItem.myQuestName;

        Debug.Log($"Player just completed quest: {tempQuestName}");
        
        // find the completed quest in the list
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].myQuestName == tempQuestName)
            {           
                // debug
                Debug.Log($"Found quest {questList[i].myQuestName} in List.");

                // change quest status
                questList[i].myQuestStatus = QuestEnums.QuestStatus.COMPLETED;

                // update to the next quest
                currentQuest = questList[i].nextQuestName;

                // break this loop
                break;
            }
        }

        // find the new quest in the list
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].myQuestName == currentQuest)
            {
                // debug
                Debug.Log($"Updating quest {questList[i].myQuestName} to ACTIVE.");

                // activate the next quest
                questList[i].myQuestStatus = QuestEnums.QuestStatus.ACTIVE;

                // break this loop
                break;
            }
        }

        Debug.Log($"Player now assigned Quest: {currentQuest}");
    }
}
