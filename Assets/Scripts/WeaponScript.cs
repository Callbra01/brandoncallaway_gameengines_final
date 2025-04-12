using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject horizontalSprite;
    public GameObject verticalSprite;

    // Start is called before the first frame update
    void Start()
    {
        horizontalSprite.SetActive(false);
        verticalSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite(int attackDir)
    {
        switch (attackDir)
        {
            case 0:
                SetVertical();
                verticalSprite.GetComponent<SpriteRenderer>().flipY = false;
                break;
            case 1:
                SetVertical();
                verticalSprite.GetComponent<SpriteRenderer>().flipY = true;
                break;
            case 2:
                SetHorizontal();
                horizontalSprite.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case 3:
                SetHorizontal();
                horizontalSprite.GetComponent<SpriteRenderer>().flipX = false;
                break;
        }
    }

    void SetHorizontal()
    {
        horizontalSprite.SetActive(true);
        verticalSprite.SetActive(false);
    }

    void SetVertical()
    {
        horizontalSprite.SetActive(false);
        verticalSprite.SetActive(true);
    }
}
