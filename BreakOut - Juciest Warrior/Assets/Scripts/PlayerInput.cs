using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private bool paused;

    public float moveSpeed = 5f;
    public GameObject levelManager;
    public GameObject pause;

	// Use this for initialization
	void Start () {
        pause.SetActive(false);
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0 && (transform.position.x > -8.05 || horizontal > 0) && (transform.position.x < 9.8 || horizontal < 0))
        {
            transform.Translate(new Vector3(horizontal, 0, 0) * Time.deltaTime * moveSpeed);
        }

        if (Input.GetButtonDown("Pause") && paused == false)
        {
            Time.timeScale = 0;
            levelManager.GetComponent<LevelManager>().gameStatus.text = "Paused";
            levelManager.GetComponent<LevelManager>().bgm.Pause();
            paused = true;
            pause.SetActive(true);
        } else if (Input.GetButtonDown("Pause") && paused == true)
        {
            Time.timeScale = 1;
            levelManager.GetComponent<LevelManager>().gameStatus.text = "Resumed";
            levelManager.GetComponent<LevelManager>().bgm.UnPause();
            paused = false;
            pause.SetActive(false);
        }
	}
}
