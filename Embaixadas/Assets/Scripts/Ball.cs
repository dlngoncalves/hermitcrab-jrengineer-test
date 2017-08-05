using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    float originalZPosition;
    
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
        Vector3 force = Vector3.up * 10;
        force.x = Random.Range(-2, 2);
        Rigidbody ball = GetComponent<Rigidbody>();
        ball.velocity = new Vector3(force.x, force.y, force.z);
        //ball.AddForce(force);        
    }

    public bool canBeKicked()
    {
        Debug.Log(transform.position.y);

        if (transform.position.y >= -0.5f && transform.position.y <= 2.5f)
            return true;
        else
          return false;
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.other.gameObject.name);
    }
}
