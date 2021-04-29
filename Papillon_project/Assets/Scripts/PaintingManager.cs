using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingManager : MonoBehaviour
{
    [SerializeField] private GameObject questItemB;
    [SerializeField] private GameObject questItemD;
    [SerializeField] private GameObject questItemF;

    [SerializeField] private GameManager myGameManager;
    [SerializeField] private PictureCamera myPictureCamera;
    [SerializeField] private Canvas myCrosshairCanvas;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        myPictureCamera = FindObjectOfType<PictureCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPictureCamera.focusOnPicture)
        {
            SetActivePaintingObjects();
        }
        else
        {
            questItemB.SetActive(false);
            questItemD.SetActive(false);
            questItemF.SetActive(false);
        }
    }

    private void SetActivePaintingObjects()
    {
        switch (myGameManager.currentQuest)
        {
            case QuestEnums.QuestName.B:
                {
                    questItemB.SetActive(true);
                    break;
                }
            case QuestEnums.QuestName.D:
                {
                    questItemD.SetActive(true);

                    break;
                }
            case QuestEnums.QuestName.F:
                {
                    questItemF.SetActive(true);
                    break;
                }
            default:
                {
                    questItemB.SetActive(false);
                    questItemD.SetActive(false);
                    questItemF.SetActive(false);
                    break;
                }
        }
    }
}
