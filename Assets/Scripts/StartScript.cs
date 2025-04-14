using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Reference vars
    public GameObject titleText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI exitText;
    public GameObject controlsText;
    public GameObject loadingSprite;

    public GameObject blackOverlay;
    float controlsTimer;
    bool controlsTimerEnabled = false;

    // Text size vars
    public float selectedTextSize;
    public float deselectedTextSize;
    int currentSelectionIndex;

    public bool isStartScreen = false;

    // Set current selection to 'Start'
    void Start()
    {
        currentSelectionIndex = 0;
        controlsTimer = 0.0f;
        loadingSprite.transform.localScale = new Vector3(0f, 0.375f, 1);
        loadingSprite.SetActive(false);
    }

    // Update Selection and Input
    void Update()
    {
        HandleInput();
        HandleSelection();
        HandleControlsTimer();
    }

    // Update both start and exit text fontSize based on selection index
    void HandleSelection()
    {
        // If 'Start' is selected, increase start text font size
        if (currentSelectionIndex == 0)
        {
            startText.fontSize = selectedTextSize;
            exitText.fontSize = deselectedTextSize;
        }

        // If 'Exit' is selected, increase exit text font size
        if (currentSelectionIndex == 1)
        {
            startText.fontSize = deselectedTextSize;
            exitText.fontSize = selectedTextSize;
        }
    }

    // Set current selection index, based on user input
    // Quit game / Change scene upon spacebar press
    void HandleInput()
    {
        // Update upward selection
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelectionIndex > 0)
            {
                currentSelectionIndex--;
            }
        }

        // Update downward selection
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelectionIndex < 1)
            {
                currentSelectionIndex++;
            }
        }

        // Check input for selection
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentSelectionIndex == 0)
            {
                if (isStartScreen)
                {
                    ShowControls();
                }
                else
                {
                    SceneManager.LoadScene(3);
                }
                
            }

            if (currentSelectionIndex == 1)
            {
                Application.Quit();
            }
        }
    }

    void HandleControlsTimer()
    {
        if (controlsTimerEnabled)
        {
            controlsTimer += Time.deltaTime;
            loadingSprite.transform.localScale = new Vector3(0f + controlsTimer * 10, 0.375f, 1);
        }


        if (controlsTimer > 2f)
        {
            SceneManager.LoadScene(3);
        }
    }

    void ShowControls()
    {
        controlsTimerEnabled = true;

        // Disable screen text
        titleText.SetActive(false);
        startText.gameObject.SetActive(false);
        exitText.gameObject.SetActive(false);

        // Display controls text
        blackOverlay.SetActive(true);
        controlsText.SetActive(true);

        // Display loading bar
        loadingSprite.SetActive(true);

    }
}
