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

public class LoadingScene : MonoBehaviour
{
    private bool sceneLoadable = true;
    private float timeElapsed = 0f;

    void Start()
    {
        //StartCoroutine(LoadYourAsyncScene());
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 3f && sceneLoadable == true)
        {
            StartCoroutine(LoadYourAsyncScene());
            sceneLoadable = false;
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Playing");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
