using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    private Vector2 force;
    private BlockDamage blockDamage;
    private LevelManager level;

    public GameObject levelManager;
    public float moveSpeed = 500f;
    public int ballDamage = 2;

	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        level = levelManager.GetComponent<LevelManager>();
        level.BallGained();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float frac;
        float angle;

        if (rigidbody2d != null && other.gameObject.CompareTag("Player"))
        {
            collider2d = other.gameObject.GetComponent<BoxCollider2D>();
            frac = (transform.position.x - other.transform.position.x) / (collider2d.size.x);

            if (frac < 0)
            {
                angle = -(1f + frac) * 90;

                if (angle > -20)
                {
                    angle = -30;
                }

                rigidbody2d.AddForce(-force);
                force = new Vector2(-Mathf.Cos(angle * (Mathf.PI / 180)), Mathf.Abs(Mathf.Sin(angle * (Mathf.PI / 180)))) * moveSpeed;
            } else
            {
                angle = (1f - frac) * 90;

                if (angle < 20)
                {
                    angle = -30;
                }

                rigidbody2d.AddForce(-force);
                force = new Vector2(Mathf.Abs(Mathf.Cos(angle * (Mathf.PI / 180))), Mathf.Abs(Mathf.Sin(angle * (Mathf.PI / 180)))) * moveSpeed;
            }

            /*Debug.Log(transform.position.x - other.transform.position.x);
            Debug.Log(frac);
            Debug.Log(angle);*/
            
            if ((force / moveSpeed).Equals(new Vector2(1f, 0f)) || (force / moveSpeed).Equals(new Vector2(-1f, 0f)))
            {
                force = new Vector2((force.x / moveSpeed), 0.3f) * moveSpeed;
            }

            level.AudioHit(2);
            rigidbody2d.AddForce(force);

            //Debug.Log(force / moveSpeed);
        }

        if (other.gameObject.CompareTag("YBounds"))
        {
            rigidbody2d.AddForce(-force);

            force = new Vector2(-force.x, force.y);
            level.AudioHit(2);
            rigidbody2d.AddForce(force);
        }

        if (other.gameObject.CompareTag("TopBound"))
        {
            rigidbody2d.AddForce(-force);

            force = new Vector2(force.x, -force.y);
            level.AudioHit(2);
            rigidbody2d.AddForce(force);
        }

        if (other.gameObject.CompareTag("BottomBound"))
        {
            level.gameStatus.text = "Damn!";
            level.AddScore(-20);
            level.BallLost();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Block"))
        {
            collider2d = other.gameObject.GetComponent<BoxCollider2D>();
            blockDamage = other.gameObject.GetComponent<BlockDamage>();

            if (transform.position.y > (other.transform.position.y - collider2d.size.y + 0.1f) 
                && transform.position.y < (other.transform.position.y + collider2d.size.y - 0.1f))
            {
                rigidbody2d.AddForce(-force);
                force = new Vector2(-force.x, force.y);
                rigidbody2d.AddForce(force);
            } else
            {
                rigidbody2d.AddForce(-force);
                force = new Vector2(force.x, -force.y);
                rigidbody2d.AddForce(force);
            }

            level.gameStatus.text = "Nice!";
            level.AddScore(10);
            level.AudioHit(1);
            blockDamage.Damage(ballDamage);
        }
    }

    public void Launch(Vector2 direction)
    {
        force = direction * moveSpeed;
        rigidbody2d.AddForce(force);
    }
}
