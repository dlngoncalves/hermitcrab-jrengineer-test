using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    float originalZPosition;
    public bool gameStarted = false;

    float lastHeight;
    public bool goingDown = false;

    GameController controller;

    // Use this for initialization
    void Start () {
        originalZPosition = transform.position.z;
        lastHeight = transform.position.y;
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		//Keep ball in the same plane
        Vector3 newPosition = transform.position;
        newPosition.z = originalZPosition;
        transform.position = newPosition;

        if (lastHeight > transform.position.y){
            goingDown = true;
        }

        lastHeight = transform.position.y;
	}

    public void KickBall()
    {        
        Vector3 force = Vector3.up * 5;
        int direction = Random.Range(0, 1);// 0 vai para a esquerda, 1 para a direita

        //Não Utilizando por enquanto
        //if (direction == 0)
        //    force.x = -0.5f;
        //else
        //    force.x = 0.5f;

        Rigidbody ball = GetComponent<Rigidbody>();
        ball.velocity = new Vector3(force.x, force.y, force.z);
        //ball.AddForce(force);
    }


    //using this for the trick position
    public bool canBeKicked()
    {
        if (transform.position.y <= controller.currentTrick.startHeight && transform.position.y >= controller.currentTrick.endHeight)
            return true;
        else
            return false;        

        //if (transform.position.y >= -0.5f && transform.position.y <= 1.5f)
        //    return true;
        //else
        //  return false;
    }

    public void applyForce(float direction, float force)
    {
        Vector3 forceToApply = new Vector3(direction*force, 0.0f, 0.0f);
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(forceToApply);
            
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (gameStarted && collision.other.gameObject.name == "Ground")
        {            
          //only if trick has not ended
           //controller.gameOver();
        }
    }
}
