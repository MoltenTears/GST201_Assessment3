using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private GameManager myGameManager;

    [Header("Chest Variables")]
    [SerializeField] private bool isChest;
    [SerializeField] private float rotateChestLidByDeg;
    private bool unlockedChestOnce;


    [Header("Door Variables")]
    [SerializeField] private bool isDoor;
    [SerializeField] private float rotateDoorByDeg;
    private bool unlockedDoorOnce;


    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the global bool has been triggered, and this script hasn't run the open method yet...
        if (myGameManager.hasOpenedChest && isChest && !unlockedChestOnce)
        {
            unlockedChestOnce = true;

            // ... run the iTween
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "islocal", true,
                "rotation", new Vector3(rotateChestLidByDeg, 0, 0),
                "time", 2.0f,
                "easetype", iTween.EaseType.linear
                ));
        }

        // if the global bool has been triggered, and this script hasn't run the open method yet...
        if (myGameManager.hasShownBearToBoy && isDoor && !unlockedDoorOnce)
        {
            Debug.Log("Door opens!");
            unlockedDoorOnce = true;

            // ... run the iTween
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "islocal", true,
                "rotation", new Vector3(0, rotateDoorByDeg, 0),
                "time", 2.0f,
                "easetype", iTween.EaseType.linear
                ));
        }

    }
}
