using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public bool canEditText;
    public float baseModifier, baseScore, currentModifier, currentScore, modCountdown, scoreAmp, displayScore;
    public float[] levelScores;
    public Scene currentScene;
    public string scoreForDisplay, sceneName;
    public TMP_Text scoreText;

    private int levelCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        levelScores = new float[2];
        levelCount = 0;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name.ToString();

        Debug.Log(currentScene.name);

        
        if (currentScene.name == "Controls")
        {
            scoreText = null;
            canEditText = false;
        }
        else if (currentScene.name == "Credits")
        {
            scoreText = null;
            canEditText = false;
        }
        else if (currentScene.name == "MainMenu")
        {
            scoreText = null;
            canEditText = false;
            ResetScore();
            levelCount = 0;
            displayScore = 0;
            for (int i = 0; i < levelScores.Length; i++)
            {
                levelScores[i] = 0;
            }
        }
        else if (currentScene.name == "WinScreen")
        {
            storeScore();

            scoreText = GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
            canEditText = false;

            float scoreSum = 0;
            if (levelScores.Length >= 1)
            {
                GameObject.Find("Level 1").GetComponent<TMPro.TextMeshProUGUI>().text = levelScores[0].ToString();
                scoreSum += levelScores[0];
            }
            if (levelScores.Length >= 2)
            {
                GameObject.Find("Level 2").GetComponent<TMPro.TextMeshProUGUI>().text = levelScores[1].ToString();
                scoreSum += levelScores[1];
            }
            Debug.Log(scoreSum);
            scoreText.text = scoreSum.ToString();

        }
        else
        {
            if (sceneName == "Level_2")
            {
                storeScore();
            }
            scoreText = GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
            canEditText = true;
        }
        

    }

    void Start()
    {

    }

    void Update()
    {
        if (modCountdown > 0)
        {
            modCountdown = modCountdown - (1 * Time.deltaTime);

        }
        if (modCountdown <= 0)
        {
            modCountdown = 0;
            currentModifier = baseModifier;
        }

        if (canEditText == true)
        {
            displayScore = currentScore * 100f;
            scoreForDisplay = displayScore.ToString();
            scoreText.text = scoreForDisplay;
        }    

        
    }


    public void AddScore(float scoreAdd)
    {
        currentScore = currentScore + (scoreAdd * currentModifier);

    }
    public void AddModifier(float modChange, float countdownAdd)
    {
        modCountdown = modCountdown + countdownAdd;
        currentModifier = currentModifier + modChange;


    }
    public void ResetScore()
    {
        currentScore = baseScore;
        currentModifier = baseModifier;

    }

    public void storeScore()
    {
        Debug.Log("Storing Score");
        levelScores[levelCount] = displayScore;
        levelCount++;
        ResetScore();
    }

}
