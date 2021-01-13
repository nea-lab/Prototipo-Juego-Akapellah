/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_Dots : MonoBehaviour
{
    public GameObject dot1;
    public GameObject dot2;
    public GameObject dot3;

    private void Start()
    {
        dot1.SetActive(false);
        dot2.SetActive(false);
        dot3.SetActive(false);
    }

    void Update()
    {
        AnimateDots();
    }

    private float elapsedTime = 0f;
    void AnimateDots()
    {
        elapsedTime = elapsedTime + Time.deltaTime;
        
        if (elapsedTime >= 0.5f && elapsedTime < 1f)
        {
            dot1.SetActive(true);
        } 
        else if(elapsedTime >= 1f && elapsedTime < 1.5f)
        {
            dot2.SetActive(true);
        }
        else if (elapsedTime >= 1.5f && elapsedTime < 2f)
        {
            dot3.SetActive(true);
        }
        else if (elapsedTime >= 2f)
        {
            dot1.SetActive(false);
            dot2.SetActive(false);
            dot3.SetActive(false);
            elapsedTime = 0f;
        }
    }
}
