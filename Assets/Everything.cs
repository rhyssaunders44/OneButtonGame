using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Everything : MonoBehaviour
{
    public Text colourWord;
    public string[] colourNames;
    int wordInt;
    int frames;
    public float score;
    float scoreOffset;
    float timeRemaining;
    bool playing;
    int d3;
    float startTime;
    public Color[] colours;
    public Image timerBar;
    int colourInt;
    bool correctAnswer;
    float timeHolder;
    public Text frameText;
    public Text Score;
    public GameObject Menu;
    public GameObject GameOverPanel;
    public Image colourDisplay;
    public GameObject colourNamePanel;
    public Text inGameColourText;
    public GameObject OnScreenScore;
    public GameObject OnScreenFrames;
    public Text onScreenScoreText;
    public Text onScreenFrameText;
   

    public void Start()
    {
        colours = new Color[] {Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow};
        colourNames = new string[] {"Black", "Blue", "Cyan", "Gray", "Green", "Magenta", "Red", "Yellow"};
        Randomiser();
        playing = false;
        timeRemaining = 10f;
        GameOverPanel.SetActive(false);
        Menu.SetActive(true);
        colourNamePanel.SetActive(false);
        OnScreenFrames.SetActive(false);
        OnScreenScore.SetActive(false);
    }

    public void Play()
    {
        Randomiser();
        playing = true;
        Menu.SetActive(false);
        colourNamePanel.SetActive(true);
        colourDisplay.color = colours[colourInt];
        startTime = Time.time;
        scoreOffset = Time.time;
        colourNamePanel.SetActive(true);
        OnScreenFrames.SetActive(true);
        OnScreenScore.SetActive(true);
        InGameUIUpdate();
    }

    public void Update()
    {
        score = (frames * 2 * (Time.time - startTime));
        colourDisplay.color = colours[colourInt];
        colourWord.text = colourNames[wordInt];



        if(playing == true)
        {
            timerBar.fillAmount = (timeRemaining + scoreOffset - Time.time) / (timeRemaining);
        }
        else
        {
            timerBar.fillAmount = 0;
        }



        if ((timerBar.fillAmount <= 0 && correctAnswer == true && playing == true) || (correctAnswer == false && Input.GetKeyDown(KeyCode.Space)) && playing == true)
        {
            GameOver();
        }


        if (correctAnswer == true && Input.GetKeyDown(KeyCode.Space) && playing == true || (timerBar.fillAmount <= 0 && correctAnswer == false) && playing == true)
        {
            Randomiser();
            
            scoreOffset = Time.time;
            timeRemaining = timeRemaining * 0.95f;
            frames++;
            Color textColour(int frameNumber) => frames >= 10 ? colourWord.color = colours[Random.Range(0, 7)] : colourWord.color = Color.black;
            colourWord.color = textColour(frames);
            InGameUIUpdate();
        }
    }

    public void Randomiser()
    {
        wordInt = Random.Range(0, 7);
        d3 = Random.Range(0, 3);

        if (d3 == 2 || d3 == 3)
        {
            colourInt = Random.Range(0, 7);

            if(colourInt == wordInt && colourInt <= 6)
            {
                wordInt++;
            }
            else if (colourInt == 7)
            {
                wordInt = Random.Range(0, 6);
            }
            correctAnswer = false;
        }
        else
        {
            colourInt = wordInt;
            correctAnswer = true;
        }
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
        playing = false;

        frameText.text = "Frames: " + frames.ToString();
        Score.text = "Score: " + ((int)score).ToString();
        OnScreenFrames.SetActive(false);
        OnScreenScore.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        playing = true;
        Randomiser();
        timeRemaining = 10f;
        frames = 0;
        score = 0;
        scoreOffset = Time.time;
        startTime = Time.time;
        GameOverPanel.SetActive(false);
        InGameUIUpdate();
        OnScreenFrames.SetActive(true);
        OnScreenScore.SetActive(true);
    }

    void InGameUIUpdate()
    {
        onScreenFrameText.text = "Frames: " + frames.ToString();
        onScreenScoreText.text = "Score: " + ((int)score).ToString();
    }
}
