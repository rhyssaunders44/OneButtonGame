using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Everything : MonoBehaviour
{
    //in game mechanic generation
    public Color[] colours;
    public string[] colourNames;
    int colourInt;
    int wordInt;
    int d3;
    public Text colourWord;

    // calculation variables & 
    float scoreOffset;
    float startTime;
    float timeRemaining;
    bool playing;
    bool correctAnswer;

    //menus
    public GameObject Menu;
    public GameObject GameOverPanel;

    //following are in game UI Elements
    public GameObject colourNamePanel;
    public Text inGameColourText;
    public GameObject OnScreenScore;
    public GameObject OnScreenFrames;
    public Text onScreenScoreText;
    public Text onScreenFrameText;
    public Image timerBar;
    public Text frameText;
    public Text Score;
    public Image colourDisplay;
    int frames;
    float score;


    public void Start()
    {
        //these arrays align the colours with the words
        colours = new Color[] {Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow};
        colourNames = new string[] {"Black", "Blue", "Cyan", "Gray", "Green", "Magenta", "Red", "Yellow"};

        //the background colour is randomised on game start
        Randomiser();

        //the game has not started yet
        playing = false;

        //dictates how long the starting time is
        timeRemaining = 10f;
    }

    public void Play()
    {
        //sets up the first frame of the game
        Randomiser();

        //we are now playing
        playing = true;

        // the score does not begin counting until you press play
        colourDisplay.color = colours[colourInt];
        startTime = Time.time;
        scoreOffset = Time.time;

        //Toggles all neccessary UI elements
        colourNamePanel.SetActive(true);
        OnScreenFrames.SetActive(true);
        OnScreenScore.SetActive(true);
        Menu.SetActive(false);
        colourNamePanel.SetActive(true);
        InGameUIUpdate();
    }

    public void Update()
    {
        //calculates the player score
        score = (frames * 2 * (Time.time - startTime));

        //changes the word and colour 
        //only changes when the the player changes it by getting a correct answer
        colourDisplay.color = colours[colourInt];
        colourWord.text = colourNames[wordInt];

        //there is no point to a pause menu in this game
        //hence game over
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
        
        //if you are playing the game, adjust the time remaining in the timer circle,
        //other wise the circle will be invisible
        if(playing == true)
        {
            timerBar.fillAmount = (timeRemaining + scoreOffset - Time.time) / (timeRemaining);
        }
        else
        {
            timerBar.fillAmount = 0;
        }


        //if you run out of time when the correct answer is up, and youre playing the game
        //you press space bar and the wrong answer is up
        //you lose
        if ((timerBar.fillAmount <= 0 && correctAnswer == true && playing == true) || (correctAnswer == false && Input.GetKeyDown(KeyCode.Space)) && playing == true)
        {
            GameOver();
        }

        //you press space when youre supposed to 
        //you dont press space when youre not supposed to 
        //the world keeps spinning
        if (correctAnswer == true && Input.GetKeyDown(KeyCode.Space) && playing == true || (timerBar.fillAmount <= 0 && correctAnswer == false) && playing == true)
        {
            // select new colour/word for the game to display
            Randomiser();
            
            //new timer fill maximum
            scoreOffset = Time.time;
            timeRemaining = timeRemaining * 0.95f;

            //reward for getting things right
            frames++;

            //if you have 10 or more successes then the game gets harder by changing the colour of the word randomly
            Color textColour(int frameNumber) => frames >= 10 ? colourWord.color = colours[Random.Range(0, 7)] : colourWord.color = Color.black;
            colourWord.color = textColour(frames);

            //update the in game scores to reflect how well youre doing
            InGameUIUpdate();
        }
    }

    public void Randomiser()
    {
        // a random selection from the array
        wordInt = Random.Range(0, 7);

        // 2/3 chance to missmatch the colour and the word
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
            // tells the game that if the player presses space now they did the wrong thing
            correctAnswer = false;
        }
        else
        {
            // tells the game that if the player presses space now they did the right thing
            colourInt = wordInt;
            correctAnswer = true;
        }
    }

    void GameOver()
    {
        // toggles UI elements
        GameOverPanel.SetActive(true);
        OnScreenFrames.SetActive(false);
        OnScreenScore.SetActive(false);

        // we arent playing
        playing = false;

        // display the endgame scores 
        frameText.text = "Frames: " + frames.ToString();
        Score.text = "Score: " + ((int)score).ToString();

    }

    // this makes the application breathe its last
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// when the game is over the player has the option to retry
    /// </summary>
    public void Retry()
    {
        //we are now playing the game again
        playing = true;

        //resets all necessary game mechanics
        Randomiser();
        timeRemaining = 10f;
        frames = 0;
        score = 0;
        scoreOffset = Time.time;
        startTime = Time.time;

        //toggles all necessary UI elements 
        GameOverPanel.SetActive(false);
        InGameUIUpdate();
        OnScreenFrames.SetActive(true);
        OnScreenScore.SetActive(true);
    }

    // whenever this is called the game updates the scores
    void InGameUIUpdate()
    {
        onScreenFrameText.text = "Frames: " + frames.ToString();
        onScreenScoreText.text = "Score: " + ((int)score).ToString();
    }
}
