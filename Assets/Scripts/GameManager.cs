using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel;

    // Start is called before the first frame update
    private void Awake()
    {
        currentLevel = 1;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleLevels();
    }

    void HandleLevels()
    {
        
        if (currentLevel == 2)
        {
            // load level 2
        }
        else if (currentLevel == 3)
        { 
            // load level 3
        }
    }
}
