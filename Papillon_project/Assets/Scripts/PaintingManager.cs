using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingManager : MonoBehaviour
{
    [SerializeField] private GameObject questItemE;

    [SerializeField] private GameManager myGameManager;
    [SerializeField] private PictureCamera myPictureCamera;
    [SerializeField] private CursorSnap myCursorSnap;
    [SerializeField] private Canvas myCrosshairCanvas;

    [SerializeField] private bool cursorSnapped;

    [Header("Quest Manager")]
    [SerializeField] private QuestItem myQuestItem;


    [Header("Painting Details")]
    [SerializeField] public QuestEnums.QuestName currentQuest;
    [SerializeField] private Image paintingImage;
    [SerializeField] private float paintingTransitionInSecondsShort = 2;
    [SerializeField] private float paintingTransitionInSecondsLong = 4;
    [SerializeField] private bool shownApple;
    [SerializeField] private bool shownTable;
    [SerializeField] private bool shownKey;
    [SerializeField] private bool shownBear;
    [SerializeField] private bool gotBear;


    [Header("Painting Images")]
    [SerializeField] private Sprite paintingSprite0;
    [SerializeField] private Sprite paintingSprite1;
    [SerializeField] private Sprite paintingSprite2;
    [SerializeField] private Sprite paintingSprite3;
    [SerializeField] private Sprite paintingSprite4;
    [SerializeField] private Sprite paintingSprite5;
    [SerializeField] private Sprite paintingSprite6;
    [SerializeField] private Sprite paintingSprite7;
    [SerializeField] private Sprite paintingSprite8;
    [SerializeField] private Sprite paintingSprite9;
 

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        myPictureCamera = FindObjectOfType<PictureCamera>();
        myCursorSnap = GetComponent<CursorSnap>();
        paintingImage = GetComponentInChildren<Image>();
        myQuestItem = GetComponent<QuestItem>();
    }

    // Update is called once per frame
    void Update()
    {
        currentQuest = myGameManager.currentQuest;

        PaintingFocus();
        PaintingImage();
    }

    private void PaintingImage()
    {
        switch (currentQuest)
        {
            case QuestEnums.QuestName.NOT_STARTED: // blank canvas
                {
                    if (paintingSprite0 != null)
                    {
                        paintingImage.sprite = paintingSprite0;
                    }
                    break;
                }
            case QuestEnums.QuestName.A: // fetch apple
                {
                    // Player sees boy reaching for an apple, looking sad. Apple appears in the room
                    if (paintingSprite1 != null)
                    {
                        paintingImage.sprite = paintingSprite1;
                    }
                    // change to the next return quest once the player has been shown the apple
                    if (!shownApple)
                    {
                        shownApple = true;
                        ChangeQuest(QuestEnums.QuestName.B);
                    }
                    break;
                }
            case QuestEnums.QuestName.B: // return apple to boy
                {
                    myGameManager.hasApple = true;

                    // while waiting for player to return to painting, nothing changes in painting
                    break;
                }
            case QuestEnums.QuestName.C: // check map on table
                {
                    myGameManager.hasApple = false;

                    // Player gives boy apple. Boy is happy (wait delay) gives them a treasure map (wait delay) boy inidcates table
                    if (!shownTable)
                    {
                        shownTable = true;
                        StartCoroutine(ShowMapAndTable());
                    }
                    break;
                }
            case QuestEnums.QuestName.D: // tell boy about chest
                {
                    myGameManager.hasMap = true;
                    // player interacts with map, finds image of chest
                    // no change to image
                    break;
                }
            case QuestEnums.QuestName.E: // boy tells player about key
                {
                    // player returns to boy, tells boy about chest,              
                    if (!shownKey)
                    {
                        shownKey = true;
                        
                        StartCoroutine(ShowKeyAndTree());
                    }
                    break;
                }
            case QuestEnums.QuestName.F: // player gets key from tree
                {
                    myGameManager.hasMap = false;
                    myGameManager.hasKey = true;

                    // once the player acquires the key from the painting, the boy cenebrates and reminds the player about the chest
                    if (paintingSprite7 != null)
                    {
                        paintingImage.sprite = paintingSprite7;
                    }
                    break;
                }
            case QuestEnums.QuestName.G: // open chest
                {
                    

                    // once the player opens the chest, 
                    if (!shownBear)
                    {
                        shownBear = true;
                        // change quest to receive new item
                        myGameManager.UpdateQuests(myQuestItem);
                        ChangeQuest(QuestEnums.QuestName.I);
                        myQuestItem.myQuestStatus = QuestEnums.QuestStatus.ACTIVE;
                    }
                    break;
                }
            case QuestEnums.QuestName.H: // pickup bear
                {
                    myGameManager.hasBear = true;

                    // no change to painting
                    if (gotBear && paintingSprite8 != null)
                    {
                        paintingImage.sprite = paintingSprite8; // change sprite: boy with Butterfly
                        myGameManager.UpdateQuests(myQuestItem);
                    }
                    break;
                }
            case QuestEnums.QuestName.I: // show boy bear
                {
                    if (paintingSprite9 != null)
                    {
                        paintingImage.sprite = paintingSprite9; // change sprite: boy waving goodbye
                    }
                    break;
                }
            case QuestEnums.QuestName.DOOR: // farewell boy
                {

                    break;
                }
            default:
                {

                    break;
                }
        }
    }

    public void GetBear()
    {
        gotBear = true;
    }

    private void ChangeQuest(QuestEnums.QuestName _NewQuest)
    {
        myQuestItem.myQuestStatus = QuestEnums.QuestStatus.INACTIVE; // deactivate the previous quest
        myQuestItem.myQuestName = _NewQuest; // change the painting's quest to the new one
        myQuestItem.nextQuestName = _NewQuest + 1; // change the NEXTQUEST value to next in sequence
    }

    private IEnumerator ShowKeyAndTree()
    {
        if (paintingSprite5 != null)
        {
            paintingImage.sprite = paintingSprite5; // change sprite: boy thinking about key
        }
        yield return new WaitForSeconds(paintingTransitionInSecondsLong);
        if (paintingSprite6 != null)
        {
            paintingImage.sprite = paintingSprite6; // change sprite: boy tells player to look in tree
        }

        // activate in-painting questItemE
        questItemE.GetComponent<Activate>().GetReferences();
        questItemE.GetComponent<QuestItem>().myQuestStatus = QuestEnums.QuestStatus.ACTIVE;
        myGameManager.questList.Add(questItemE.GetComponent<QuestItem>());

        // change quest to receive new item
        myGameManager.UpdateQuests(myQuestItem);
        ChangeQuest(QuestEnums.QuestName.I);
    }

    private IEnumerator ShowMapAndTable()
    {
        if (paintingSprite2 != null)
        {
            paintingImage.sprite = paintingSprite2; // change sprite: boy happy with apple
        }
        yield return new WaitForSeconds(paintingTransitionInSecondsLong);
        if (paintingSprite3 != null)
        {
            paintingImage.sprite = paintingSprite3; // change sprite: boy offers map
        }
        yield return new WaitForSeconds(paintingTransitionInSecondsShort);
        if (paintingSprite4 != null)
        {
            paintingImage.sprite = paintingSprite4; // change sprite: boy recommends table
        }

        //change quest to receive new items
        myGameManager.UpdateQuests(myQuestItem);
        ChangeQuest(QuestEnums.QuestName.D);
    }

    private void PaintingFocus()
    {
        if (myPictureCamera.focusOnPicture)
        {
            SetActivePaintingObjects();
            if (!cursorSnapped)
            {
                cursorSnapped = true;
                myCursorSnap.CentreCursor();
            }
        }
        else
        {
            cursorSnapped = false;
            questItemE.SetActive(false);
        }
    }

    private void SetActivePaintingObjects()
    {
        switch (myGameManager.currentQuest)
        {
            case QuestEnums.QuestName.E:
                {
                    questItemE.SetActive(true);
                    break;
                }
            default:
                {
                    questItemE.SetActive(false);
                    break;
                }
        }
    }
}
