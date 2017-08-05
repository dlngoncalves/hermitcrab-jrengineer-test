using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    float originalZPosition;
    public bool gameStarted = false;
	// Use this for initialization
	void Start () {
        originalZPosition = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		//Keep ball in the same plane
        Vector3 newPosition = transform.position;
        newPosition.z = originalZPosition;
        transform.position = newPosition;

	}

    public void KickBall()
    {        
        Vector3 force = Vector3.up * 5;
        force.x = Random.Range(-2, 2);
        Rigidbody ball = GetComponent<Rigidbody>();
        ball.velocity = new Vector3(force.x, force.y, force.z);
        //ball.AddForce(force);        
    }

    public bool canBeKicked()
    {
        Debug.Log(transform.position.y);

        if (transform.position.y >= -0.5f && transform.position.y <= 1.5f)
            return true;
        else
          return false;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (gameStarted && collision.other.gameObject.name == "Ground")
        {
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.LoadEndScreen(0.5f);
        }
    }
}
