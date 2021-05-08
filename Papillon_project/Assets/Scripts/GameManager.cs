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

    [Header("Quest Items")]
    [SerializeField] public bool hasApple;
    [SerializeField] public bool hasMap;
    [SerializeField] public bool hasKey;
    [SerializeField] public bool hasOpenedChest;
    [SerializeField] public bool hasBear;
    [SerializeField] public bool hasShownBearToBoy;

    [SerializeField] private GameObject appleGO;
    [SerializeField] private GameObject appleLight;

    [SerializeField] private GameObject mapGO;
    [SerializeField] private GameObject mapLight;

    [SerializeField] private GameObject keyLight;

    [SerializeField] private GameObject chestLight;

    [SerializeField] private GameObject bearGO;
    [SerializeField] private GameObject bearLight;

    [SerializeField] private GameObject doorLight;

    private void Start()
    {
        GetQuests();
        DeactivateAll();
    }

    private void DeactivateAll()
    {
        appleGO.SetActive(false);
        appleLight.SetActive(false);

        mapGO.SetActive(false);
        mapLight.SetActive(false);

        chestLight.SetActive(false);

        doorLight.SetActive(false);

    }

    private void Update()
    {
        ChangeLights();
    }

    public void ActivateSet(GameObject _questGameObject, GameObject _questLight)
    {
        _questGameObject.SetActive(true);
        _questLight.SetActive(true);
    }

    public void ActivateLight(GameObject _questLight)
    {
        _questLight.SetActive(true);
    }

    private void DeactivateObject(GameObject _questGameObject)
    {
        _questGameObject.SetActive(false);
    }

    public void ActivateMapSet()
    {
        mapGO.SetActive(true);
        mapLight.SetActive(true);
    }

    public void ActivateKeyLight()
    {
        keyLight.SetActive(true);
    }

    private void ChangeLights()
    {
        switch (currentQuest)
        {
            case QuestEnums.QuestName.NOT_STARTED:
                {

                    break;
                }
            case QuestEnums.QuestName.A:
                {
                    ActivateSet(appleGO, appleLight);
                    break;
                }
            case QuestEnums.QuestName.B:
                {
                    DeactivateObject(appleGO);
                    break;
                }
            case QuestEnums.QuestName.C:
                {
                    
                    break;
                }
            case QuestEnums.QuestName.D:
                {
                    DeactivateObject(mapGO);
                    break;
                }
            case QuestEnums.QuestName.E:
                {

                    break;
                }
            case QuestEnums.QuestName.F:
                {
                    ActivateLight(chestLight);
                    break;
                }
            case QuestEnums.QuestName.G:
                {
                    break;
                }
            case QuestEnums.QuestName.H:
                {
                    break;
                }
            case QuestEnums.QuestName.I:
                {
                    break;
                }
            case QuestEnums.QuestName.END:
                {
                    DeactivateObject(bearGO);
                    ActivateLight(doorLight);
                    break;
                }
            default:
                {
                    Debug.LogError("GameManager.ChangeLights() switch defaulted; check for errors.");
                    break;
                }
        }
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
        QuestEnums.QuestName currentQuestName = _questItem.myQuestName;

        // Debug.Log($"Player just completed quest: {currentQuestName}");
        
        // find the completed quest in the list
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].myQuestName == currentQuestName)
            {           
                // debug
                // Debug.Log($"Found quest {questList[i].myQuestName} in List.");

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
                // Debug.Log($"Updating quest {questList[i].myQuestName} on {questList[i].name} to ACTIVE.");

                // activate the next quest
                questList[i].myQuestStatus = QuestEnums.QuestStatus.ACTIVE;

                // break this loop
                break;
            }
        }
    }
}
