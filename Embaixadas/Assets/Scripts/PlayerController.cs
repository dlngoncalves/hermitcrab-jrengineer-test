using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool canKick = false;    
    bool gameStarted = false;

    Ball ball;
    GameController controller;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
	}	

    void Update () {
        if (Input.GetMouseButtonDown(0)){
            if (gameStarted){
                canKick = ball.canBeKicked();
            }
            else{
                canKick = true;
                gameStarted = true;
                ball.gameStarted = true;
            }
           
           if(canKick){
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(cameraRay, out hit)){                
                    if (hit.transform.tag == "Ball"){                    
                        ball.KickBall();
                        controller.updateScore();
                    }
                    else{
                        wrongKick();
                    }
                }
                else{
                    wrongKick();
                }
           }
           else{
               wrongKick();
           }
        }
	}

    void wrongKick()
    {        
        Destroy(gameObject);
    }
}
