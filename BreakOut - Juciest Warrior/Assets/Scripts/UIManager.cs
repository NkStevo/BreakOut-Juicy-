using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        SceneManager.LoadScene(gameManager.level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
