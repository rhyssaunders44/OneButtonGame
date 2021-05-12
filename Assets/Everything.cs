using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Everything : MonoBehaviour
{
    public Text colourWord;
    public string[] colourNames;
    int wordInt;

    float timeRemaining;

    int d3;
    public Color32[] colours;
    public Image timerBar;
    int colourInt;
    bool correctAnswer;
    float timeHolder;

    public void Start()
    {
        Randomiser();
        timeRemaining = 30f;
    }

    public void Update()
    {
        timerBar.fillAmount = (timeHolder + timeRemaining - Time.time) / (timeHolder + timeRemaining);

        if(timeRemaining <= 0 || (correctAnswer == false && Input.GetKeyDown(KeyCode.Space)))
        {
            GameOver();
        }

        if(correctAnswer == true && Input.GetKeyDown(KeyCode.Space))
        {
            Randomiser();
            TimeCounter();
            timeRemaining = timeRemaining * 0.95f;
        }
    }

    public void Randomiser()
    {
        wordInt = Random.Range(0, 7);
        d3 = Random.Range(0, 3);

        if (d3 == 2 || d3 == 3)
        {
            colourInt = Random.Range(0, 7);
            correctAnswer = false;
        }
        else
        {
            colourInt = wordInt;
            correctAnswer = true;
        }

    }

    void TimeCounter()
    {
        timeHolder = Time.time;
    }

    void GameOver()
    {

    }
}
