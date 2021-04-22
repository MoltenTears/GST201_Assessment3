using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject instructionsGO;
    [SerializeField] private GameObject buttonsRig;
    [SerializeField] private SceneField FirstScene;

    private void Start()
    {
        instructionsGO = GameObject.FindGameObjectWithTag("Instructions");
        if (instructionsGO)
        {
            instructionsGO.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // hard quick quit function
        {
            ExitButton();
        }
    }


    public void StartGameButton()
    {
        Debug.Log("Start Button Pressed.");

        SceneManager.LoadScene(FirstScene.SceneName);
    }

    public void InstructionsButton()
    {
        Debug.Log("Instructions Button Pressed.");

        if (instructionsGO)
        {
            instructionsGO.SetActive(true);
            buttonsRig.SetActive(false);
        }
    }

    public void ExitButton()
    {
        Debug.Log("Exit Button Pressed.");

        Application.Quit();

    }

    public void ReturnButton()
    {
        Debug.Log("Return button pressed");

        if (instructionsGO)
        {
            instructionsGO.SetActive(false);
            buttonsRig.SetActive(true);

        }
    }
}
