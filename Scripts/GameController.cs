using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] pickUps;
    public Vector3 spawnValues;
    private int hazardCount;
    private int pickupCount;
    public float spawnWait;
    public float pickupWait;
    public float startWait;
    public float waveWait;
    public float speedUp;

    public Text ScoreText;
    public Text winText;
    public Text gameOverText;
    public Text restartText;
    public int score;
    private bool gameOver;
    private bool restart;
    public AudioSource winsoundSource;
    public AudioClip winsoundOne;
    public AudioSource losesoundSource;
    public AudioClip losesoundOne;

    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    private float mScore;

    //private Mover mMover;

    private void Start()
    {
        hazardCount = 10;
        pickupCount = 1;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        timer = mainTimer;



    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.F))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }

        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            gameOverText.text = "Game Over!";
            gameOver = true;
            losesoundSource.clip = winsoundOne;
            losesoundSource.Play();
            restart = true;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        
        while (true)
        {
            for (int i = 0; i < pickupCount; i++)
            {
                GameObject pickUp = pickUps[Random.Range(0, pickUps.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
                Instantiate(pickUp, spawnPosition, spawnRotation);               
                yield return new WaitForSeconds(pickupWait);
            }

            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                
                yield return new WaitForSeconds(spawnWait);
            }



            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'F' to restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        mScore = score;
    }

    public void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
       if (score >= 200)
       {
        winText.text = "You Win! Game created by Cristian Espinoza";
        gameOver = true;
        restart = true;
        winsoundSource.clip = winsoundOne;
        winsoundSource.Play();
            canCount = false;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        losesoundSource.clip = winsoundOne;
        losesoundSource.Play();
        canCount = false;
    }

   public float CurrentScore
    {
        get { return mScore; }
    }

        
}
