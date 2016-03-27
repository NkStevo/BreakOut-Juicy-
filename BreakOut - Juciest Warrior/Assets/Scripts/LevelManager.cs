using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public Text gameStatus;
    public Text lifeText;
    public Text scoreText;
    public Text timerText;
    public int lives;
    public int blockNum;
    public bool levelOver;
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioClip victory;
    public AudioClip hit1;
    public AudioClip hit2;
    public GameObject uiManager;

    private int ballNum;
    private int score;
    private float timer;
    private float pauseTime;
    private bool isTimed;
    private PlayerSpawn playerSpawn;
    private GameObject player;
    private GameObject[] blocks;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        bgm.Play();
        Debug.Log("Start");
        player = GameObject.FindGameObjectWithTag("Player");
        blocks = GameObject.FindGameObjectsWithTag("Block");
        gameManager = GameManager.instance;
        blockNum = blocks.GetLength(0);
        playerSpawn = player.GetComponent<PlayerSpawn>();
        ballNum = 0;
        score = 0;
        lives = 3;
        timer = 200f;
        levelOver = false;
        isTimed = true;
        gameStatus.text = "Let's Go!";
        lifeText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        timerText.text = "Timer: " + timer;
        Debug.Log("Start blocks: " + blockNum);
        Debug.Log("Start lives:" + lives);
    }
	
	// Update is called once per frame
	void Update () {

        if (isTimed == true && timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + timer.ToString("f0");
        } else if (isTimed == true && timer <= 0)
        {
            gameStatus.text = "Slacker";
            AddScore(-100);
            isTimed = false;
        }
        

        if (blockNum <= 0 && !levelOver)
        {
            GameStatus(false);
        }

        if (levelOver && Time.realtimeSinceStartup - pauseTime >= 10)
        {
            uiManager.GetComponent<UIManager>().Menu();
        }
	}

    public void BallLost()
    {
        Debug.Log("Before Loss: " + ballNum);
        ballNum--;
        Debug.Log("After Loss: " + ballNum);

        if (ballNum <= 0)
        {
            lives--;
            lifeText.text = "Lives: " + lives;

            if (lives <= 0)
            {
                GameStatus(true);
            } else
            {
                playerSpawn.Invoke("SetBall", 0.5f);
            }
        }
    }

    public void BallGained()
    {
        Debug.Log("Before Gain: " + ballNum);
        ballNum++;
        Debug.Log("After Gain: " + ballNum);
    }

    private void GameStatus(bool isLoss)
    {
        levelOver = true;
        Debug.Log(lives);
        Time.timeScale = 0;
        pauseTime = Time.realtimeSinceStartup;
        gameStatus.text = isLoss ? "GAME\nOVER!" : "LEVEL\nWON!";
        bgm.Stop();
        bgm.loop = false;
        bgm.clip = victory;
        bgm.Play();
    }

    public void AudioHit(int num)
    {
        if (num == 1)
        {
            sfx.clip = hit1;
            sfx.Play();
        } else
        {
            sfx.clip = hit2;
            sfx.Play();
        }
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score: " + score;
    }
}
