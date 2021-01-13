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

public class GeneralGameController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private GameController gameController;
    public int totalPlayerScore = 0;

    private void Update()
    {
        GetPlayerScore();
    }

    private void GetPlayerScore()
    {
        Scene actualScene = SceneManager.GetActiveScene();

        if (actualScene.name == "Playing")
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            totalPlayerScore = gameController.playerScore;
            Debug.Log("General Game Controller - Total Score: " + totalPlayerScore);
        }

        if (actualScene.name == "End")
        {
            Debug.Log("General Game Controller - Total Score: " + totalPlayerScore);
        }
    }
}
