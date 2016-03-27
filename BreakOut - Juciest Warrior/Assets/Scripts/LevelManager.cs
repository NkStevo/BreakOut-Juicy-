using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public Text gameStatus;
    public int lives;
    public int blockNum;
    public bool levelOver;
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioClip victory;
    public AudioClip hit1;
    public AudioClip hit2;

    private int ballNum;
    private PlayerSpawn playerSpawn;
    private GameObject player;
    private GameObject[] blocks;

    // Use this for initialization
    void Start () {
        bgm.Play();
        Debug.Log("Start");
        player = GameObject.FindGameObjectWithTag("Player");
        blocks = GameObject.FindGameObjectsWithTag("Block");
        blockNum = blocks.GetLength(0);
        playerSpawn = player.GetComponent<PlayerSpawn>();
        ballNum = 0;
        lives = 3;
        levelOver = false;
        gameStatus.text = "";
        Debug.Log("Start blocks: " + blockNum);
        Debug.Log("Start lives:" + lives);
    }
	
	// Update is called once per frame
	void Update () {
	    if (blockNum <= 0 && !levelOver)
        {
            GameStatus(false);
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
        player.GetComponent<PlayerInput>().enabled = false;
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
}
