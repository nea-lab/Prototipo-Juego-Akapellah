/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMenuController : MonoBehaviour
{
    public GameObject menuImage;

    public GameObject niveles;
    public GameObject personajes;
    public GameObject regresar;

    public GameObject blackScreen;

    public void SetMenuImage()
    {
        if(!menuImage.activeSelf)
        {
            menuImage.SetActive(true);

            niveles.SetActive(true);
            personajes.SetActive(true);
            regresar.SetActive(true);

            blackScreen.SetActive(true);
        }
        else 
        { 
            menuImage.SetActive(false);

            niveles.SetActive(false);
            personajes.SetActive(false);
            regresar.SetActive(false);

            blackScreen.SetActive(false);
        }
    }
}
