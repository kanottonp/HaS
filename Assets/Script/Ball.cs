﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : PickableObj {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject);
    }
}
