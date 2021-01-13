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

public class GameUIController : MonoBehaviour
{
    public GameController gameControl;
    public GameObject intro, playing, gameOver;
    public AudioSource endAudio;
    public int kills;

    void Start()
    {
        kills = 0;
    }

    void Update()
    {
    }

    public void OneKill()
    {
        gameControl.AddPoints(100);
        kills += 1;
        if(kills==6)
        {
            endAudio.pitch = 1.2f;
            GameOver();
        }
    }

    public void StartPlaying()
    {
        intro.SetActive(false);
        playing.SetActive(true);
    }

    public void GameOver()
    {
        playing.SetActive(false);
        gameOver.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
