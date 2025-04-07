using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public TextMeshProUGUI exitText;
    public float selectedTextSize;
    public float deselectedTextSize;

    int currentSelectionIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSelectionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleSelection();
    }

    // Update both start and exit text fontSize based on selection index
    void HandleSelection()
    {
        if (currentSelectionIndex == 0)
        {
            startText.fontSize = selectedTextSize;
            exitText.fontSize = deselectedTextSize;
        }

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelectionIndex > 0)
            {
                currentSelectionIndex--;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelectionIndex < 1)
            {
                currentSelectionIndex++;
            }
        }

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
