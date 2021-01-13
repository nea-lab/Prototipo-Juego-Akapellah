/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public int kills;
    
    public GameObject pauseObjects;

    public GameObject backgroundVideo;

    public bool isLevelOver = false;

    void Awake()
    {
        playerScore = 0;
        scoreText.text = playerScore.ToString();
        kills = 0;

        backgroundVideo.GetComponent<VideoPlayer>().frame = 10;
    }

    private void Update()
    {
        ShowLevelIndicator();
        ShowWeedBadge();
    }

    public void AddPoints(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void OneKill(int scoreToAdd)
    {
        AddPoints(scoreToAdd);
        kills += 1;
        /*if (kills == 6)
        {
            endAudio.pitch = 1.2f;
            GameOver();
        }*/
    }

    #region Toggle Pause
    public void TogglePause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
        var videoPlayer = backgroundVideo.GetComponent<UnityEngine.Video.VideoPlayer>();
        
        if(videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else { videoPlayer.Play();}

        if (pauseObjects.activeSelf == true)
        {
            pauseObjects.SetActive(false);
        }
        else
        {
            pauseObjects.SetActive(true);
        }
    }
    #endregion

    public void ExitGame()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    #region Show Level Indicator

    public GameObject levelIndicator;
    private Transform levelIndicatorTransform;

    private float levelIndicatorTimeElapsed = 0f;
    private float levelIndicatorMovementRate = 3f;
    private float origPosY = 6f;
    private bool levelIndicatorDestroyed = false;

    public void ShowLevelIndicator()
    {
        // if (levelNumber == 1)
        //{
        if (!levelIndicatorDestroyed)
        {
            levelIndicatorTransform = levelIndicator.GetComponent<Transform>();
            levelIndicatorTimeElapsed += Time.deltaTime;
            float posY = origPosY - (levelIndicatorMovementRate * levelIndicatorTimeElapsed);
            if (posY >= -10f)
            {
                levelIndicatorTransform.position = new Vector3(levelIndicatorTransform.position.x, posY, levelIndicatorTransform.position.z);
            }
            else
            {
                Destroy(levelIndicator);
                levelIndicatorDestroyed = true;
            }
        }
        //}
    }
    #endregion

    #region Show Weed Badge

    public GameObject weedBadge;
    private bool weedBadgeActive = false;
    private float weedBadgeActiveTimeElapsed = 0f;
    public void ShowWeedBadge()
    {
        if (playerScore >= 400 && playerScore < 500 ||
            playerScore >= 4200 && playerScore < 4300)
        {
            weedBadgeActiveTimeElapsed += Time.deltaTime;

            if (!weedBadge.activeSelf && !weedBadgeActive)
            {
                weedBadge.SetActive(true);
                weedBadgeActive = true;
            }

            if (weedBadgeActive == true && weedBadgeActiveTimeElapsed >= 5f)
            {
                weedBadge.SetActive(false);
                weedBadgeActive = false;
            }  
        }
    }

    #endregion
}