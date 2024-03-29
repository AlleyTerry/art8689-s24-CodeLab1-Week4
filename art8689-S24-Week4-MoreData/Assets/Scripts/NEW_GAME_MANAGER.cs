using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NEW_GAME_MANAGER : MonoBehaviour
{
    public static NEW_GAME_MANAGER instance;

    public TextMeshProUGUI display;

    public int score;

    private const string FILE_DIR = "/DATA/";

    private const string DATA_FILE = "highScores.txt";

    private string FILE_FULL_PATH;


    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            // moved the loop somewhere else so its not constantly updating
            // only updates after the game ends
            score = value;
        }
    }

    private string highScoresString = "";

    private List<int> highScores;

    public List<int> HighScores
    {
        get
        {
            if (highScores == null && File.Exists(FILE_FULL_PATH))
            {
                //gets the numbers from file

                highScores = new List<int>();

                highScoresString = File.ReadAllText((FILE_FULL_PATH));

                highScoresString = highScoresString.Trim();

                string[] highScoreArray = highScoresString.Split("\n");

                for (int i = 0; i < highScoreArray.Length; i++)
                {
                    int currentScore = Int32.Parse(highScoreArray[i]);
                    highScores.Add((currentScore));
                }
            }
            else if (highScores == null)
            {
                Debug.Log("No new hs");
                highScores = new List<int>();
                highScores.Add(3);
                highScores.Add(2);
                highScores.Add(1);
                highScores.Add(0);
            }

            return highScores;
        }
        
    }

    private float timer = 0;
    public int maxTime = 10;

    private bool isInGame = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
            display.text = "Score: " + score + "\nTime: " + (maxTime - (int)timer);
        }
        else
        {
            display.text = "GAME OVER\nFINAL SCORE: " + score + "\nHigh Scores:\n" + highScoresString;
        }

        timer += Time.deltaTime;

        if (timer >= maxTime && isInGame)
        {
            isInGame = false;
            SceneManager.LoadScene(("EndScene"));
            SetHighScore();
        }
    }

    bool IsHighScore(int score)
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

    void SetHighScore()
    {
        if (IsHighScore(score))
        {
            int highScoreSlot = -1;

            for (int i = 0; i < HighScores.Count; i++)
            {
                if (score > highScores[i])
                {
                    highScoreSlot = i;
                    break;
                }
            }
            
            highScores.Insert(highScoreSlot, score);

            highScores = highScores.GetRange(0, 5);

            string scoreBoardText = "";

            foreach (var highScore in highScores)
            {
                scoreBoardText += highScore + "\n";
            }

            highScoresString = scoreBoardText;
            
            File.WriteAllText(FILE_FULL_PATH, highScoresString);

        }
    }
}
