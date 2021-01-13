using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Text scoreText;
    private GeneralGameController GeneralGameControllerScript;

    void Start()
    {
        GeneralGameControllerScript = GameObject.Find("GeneralGameController").GetComponent<GeneralGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score: " + GeneralGameControllerScript.totalPlayerScore;
    }
}
