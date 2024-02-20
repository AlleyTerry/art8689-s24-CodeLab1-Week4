using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            score = value;
        }
    }

    private string highScoresString = "";
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
