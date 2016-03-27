using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private int score = 0;
    public int level = 1;

    public static GameManager instance;

	// Use this for initialization
	void Awake () {
	    if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
