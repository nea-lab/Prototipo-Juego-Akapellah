/***
 * This script is part of the videogame developed for Akapellah by Nea Lab.
 * This development started in october 2020.
 * Nea Lab team owns all the rgihts of the development, the specific scripts attached to it and the art created.
 * Any replication, resell or attempt or copying is totally illegal.
***/

using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class IntroVideoControl : MonoBehaviour
{
    VideoPlayer videoplayer;
    //public GameObject intro1, intro2;
    public ScenesController scenesController;

    IEnumerator Start()
    {
        videoplayer = GetComponent<VideoPlayer>();
        videoplayer.loopPointReached += EndReached;
        videoplayer.Prepare();

        while (!videoplayer.isPrepared)
            yield return null;

        videoplayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        /*
        intro2.SetActive(true);
        intro1.SetActive(false);
        */
        scenesController.LoadIntroScene();
    }
}