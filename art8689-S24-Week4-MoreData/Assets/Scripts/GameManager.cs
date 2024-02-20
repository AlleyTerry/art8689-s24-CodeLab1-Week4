using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //making it a singleton
    public static GameManager instance;
    public int score; 
    
    //creates a file path script variables
    //the folder where the txt file will be
    const string FILE_DIR = "/DATA/";
    //the file itself
    const string DATA_FILE = "highScores.txt";
    //this is dependent on the operating system
    string FILE_FULL_PATH;

    //Score property
    public int Score
    {
        // gets value from lowercase score
        get
        {
            return score;
        }

        set
        {
            score = value;
            
            //check if the current score is a high score
            //decides where to put it in our high score list
            if (isHighScore(score))
            {
                //everytime the score is greater than the previous high score it will add all of those numbers.
                //instead it should only check the score after the game is finished
                //replaced so it includes 0
                int highScoreSlot = -1;
                //loop through the HighScores list
                for (int i = 0; i < HighScores.Count; i++)
                {
                    if (score > highScores[i])
                    {
                        highScoreSlot = i;
                        break;
                    }
                }
                //puts it in the list
                highScores.Insert(highScoreSlot, score);
                // only 5 scores recorded
                highScores = highScores.GetRange(0, 5);

                string scoreBoardText = "";
                
                //for each goes in order and does not check what spot it is in
                foreach (var highScore in highScores)
                {
                    // so each high score gets its own line
                    scoreBoardText += highScore + "\n";
                }

                highScoresString = scoreBoardText;
                //check to see if the directory exists:
                if (!Directory.Exists(Application.dataPath + FILE_DIR))
                {
                    Directory.CreateDirectory(Application.dataPath + FILE_DIR);
                }
                // write the scores to the txt file
                File.WriteAllText(FILE_FULL_PATH, highScoresString);
                
                
            }
            
        }
    }
    
    //declare an empty string where the hs txt will go
    private string highScoresString = "";
    
    //list of all our high scores
    List<int> highScores;
    
    //property to make the highScore list
    public List<int> HighScores
    {
        get
        {
            //if empty...
            if (highScores == null)
            {
                //make a new list
                highScores = new List<int>();
                //had to initialize a list in order to get the text file to exist
                //so now I'm commenting these out
                highScores.Add(0);
                highScores.Insert(0,3);
                highScores.Insert(1,2);
                highScores.Insert(2,1);
                //taking the high score values we have saves

                if (File.Exists((FILE_FULL_PATH)))
                {
                    highScoresString = File.ReadAllText(FILE_FULL_PATH);

                    highScoresString = highScoresString.Trim();
                
                    //splits it up based off of a character of our choosing
                    string[] highScoreArray = highScoresString.Split("\n");
                
                    //go through the array turn each string into a number
                    for (int i = 0; i < highScoreArray.Length; i++)
                    {
                        int currentScore = Int32.Parse(highScoreArray[i]);
                        highScores.Add(currentScore);
                    }
                }
              
            }

            return highScores;
        }

      
    }

    bool isInGame = true;
    
    
    
    public TextMeshProUGUI display;
    public float timer = 0;
    public float maxTime = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy((gameObject));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FILE_FULL_PATH = Application.dataPath + FILE_DIR + DATA_FILE;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            display.text = "Score: " + score + "\nTime: " + (maxTime - (int)timer) ;

        }
        else
        {
            display.text = "GAME OVER\nFINAL SCORE: " + score + "\nHigh Scores:\n " + highScoresString;
        }
        timer += Time.deltaTime;

        if (timer >= maxTime && isInGame)
        {
            isInGame = false;
            SceneManager.LoadScene("EndScene");
        }
    }

    //
    bool isHighScore(int score)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i] < score)
            {
                return true;
            }
        }

        return false;
    }
}
