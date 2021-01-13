/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    private void Awake()
    {
       DontDestroyOnLoad(this);
    }

    public void LoadPlayingScene()
    {
        Debug.Log("PlayingScene Button Preseed");
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
