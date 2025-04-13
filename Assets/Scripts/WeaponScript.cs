using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject horizontalSprite;
    public GameObject verticalSprite;

    // Set both weapon sprites to non active
    void Start()
    {
        horizontalSprite.SetActive(false);
        verticalSprite.SetActive(false);
    }


    public void UpdateSprite(int attackDir)
    {
        // Update sprite and flip x & y when applicable
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

    // Enable horizontal sprite and disable vertical sprite
    void SetHorizontal()
    {
        horizontalSprite.SetActive(true);
        verticalSprite.SetActive(false);
    }

    // Enable vertical sprite and disable horizontal sprite
    void SetVertical()
    {
        horizontalSprite.SetActive(false);
        verticalSprite.SetActive(true);
    }
}
