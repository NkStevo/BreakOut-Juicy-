using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
    private static int ballNum = 0;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    private Vector2 force;

    public float moveSpeed = 500f;

	// Use this for initialization
	void Start () {
        ballNum++;
        rigidbody2d = GetComponent<Rigidbody2D>();
        force = Vector2.down * moveSpeed;
        rigidbody2d.AddForce(force);
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        float frac;
        float angle;

        if (other.gameObject.CompareTag("Player"))
        {
            collider2d = other.gameObject.GetComponent<BoxCollider2D>();
            frac = (transform.position.x - other.transform.position.x) / (collider2d.size.x / 2f);
            angle = (1f - frac) * 90;
            Debug.Log(frac);
            Debug.Log(angle);

            rigidbody2d.AddForce(-force);
            force = new Vector2(Mathf.Cos(angle * (Mathf.PI / 180)), (-force.y / moveSpeed)) * moveSpeed;
            rigidbody2d.AddForce(force);

            Debug.Log(force / moveSpeed);
        }
    }
}
