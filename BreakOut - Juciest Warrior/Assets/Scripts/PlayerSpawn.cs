using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {
    private bool launched = false;
    private BoxCollider2D paddleCollider2d;
    private CircleCollider2D ballCollider2d;
    private BallCollision ballCollision;

    public GameObject ball;

	// Use this for initialization
	void Start () {
        SetBall();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Launch") && launched == false)
        {
            ballCollision = ball.GetComponent<BallCollision>();
            ballCollision.Launch(Vector2.up);
            ball.transform.parent = null;
            launched = true;
        }
	}

    public void SetBall()
    {
        launched = false;
        paddleCollider2d = GetComponent<BoxCollider2D>();
        ballCollider2d = ball.GetComponent<CircleCollider2D>();
        ball = (GameObject)Instantiate(ball, new Vector3(transform.position.x,
            (transform.position.y + paddleCollider2d.size.y + ballCollider2d.radius),
            transform.position.z), transform.rotation);
        ball.transform.parent = transform;
    }
}
