﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    private Rigidbody2D rigidbody2d;

    public float moveSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            transform.Translate(new Vector3(horizontal, 0, 0) * Time.deltaTime * moveSpeed);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}