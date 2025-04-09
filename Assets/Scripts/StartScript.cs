using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Reference vars
    public TextMeshProUGUI startText;
    public TextMeshProUGUI exitText;

    // Text size vars
    public float selectedTextSize;
    public float deselectedTextSize;
    int currentSelectionIndex;

    // Set current selection to 'Start'
    void Start()
    {
        currentSelectionIndex = 0;
    }

    // Update Selection and Input
    void Update()
    {
        HandleInput();
        HandleSelection();
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
                SceneManager.LoadScene(3);
            }

            if (currentSelectionIndex == 1)
            {
                Application.Quit();
            }
        }
    }
}
