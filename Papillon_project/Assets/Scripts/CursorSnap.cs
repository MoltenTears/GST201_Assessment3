using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorSnap : MonoBehaviour
{
    [SerializeField] PaintingManager myPaintingManager;

    // Start is called before the first frame update
    void Start()
    {
        myPaintingManager = FindObjectOfType<PaintingManager>();
    }

    public void CentreCursor()
    {
        // Debug.Log("CursorSanp.CentreCursor() called.");

        Vector2 mousePos = new Vector2((Screen.width / 2), Screen.height / 2); // get the centre of the screen

        Mouse.current.WarpCursorPosition(mousePos); // warp the cursor to the above 2D coordinates
    }
}
