using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;

    private const string FILE_DIR = "/DATA";
    private const string FILE_DATA = "highScores.txt";
    private string FILE_FULL_PATH;
    
    //list of all our high scores
    List<int> highScores;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            if (isHighScore(score))
            {
                
                //replaced
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
                highScores.Insert(highScoreSlot,score);
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
                
                File.WriteAllText(FILE_FULL_PATH, highScoresString);
                
            }
            
        }
    }

    private string highScoresString = "";
    
    
    //property to make the highScore list
    public List<int> HighScores
    {
        get
        {
            if (highScores == null)
            {
                highScores = new List<int>();
                
                //taking the high score values we have saves
                highScoresString = File.ReadAllText((FILE_FULL_PATH));

                highScoresString = highScoresString.Trim();
                
                //splits it up based off of a chracter of our choosing
                string[] highScoreArray = highScoresString.Split("\n");
                
                //go through the array turn each string into a number
                for (int i = 0; i < highScoreArray.Length; i++)
                {
                    int currentScore = Int32.Parse(highScoreArray[i]);
                    highScores.Add(currentScore);
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
        //FILE_FULL_PATH = Application.dataPath
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
            display.text = "GAME OVER " + "\nFINAL SCORE: " + score + "\nHighScores:\n " + highScoresString;
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
