using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {
    private bool launched = false;
    private BoxCollider2D paddleCollider2d;
    private CircleCollider2D ballCollider2d;
    private BallCollision ballCollision;

    public GameObject ball;
    public GameObject newBall;

	// Use this for initialization
	void Start () {
        SetBall();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Launch") && launched == false)
        {
            ballCollision = newBall.GetComponent<BallCollision>();
            ballCollision.Launch(Vector2.up);
            newBall.transform.parent = null;
            launched = true;
        }
	}

    public void SetBall()
    {
        launched = false;
        paddleCollider2d = GetComponent<BoxCollider2D>();
        ballCollider2d = ball.GetComponent<CircleCollider2D>();
        Debug.Log(transform.position);
        newBall = (GameObject)Instantiate(ball, new Vector3(transform.position.x,
            (transform.position.y + paddleCollider2d.size.y + ballCollider2d.radius + 0.07f),
            transform.position.z), transform.rotation);
        newBall.transform.parent = transform;
    }
}
