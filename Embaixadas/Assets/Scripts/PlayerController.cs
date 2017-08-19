using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    bool canKick = false;    
    bool gameStarted = false;
    bool isMobile = false;

    Ball ball;
    GameController controller;
    
	// Use this for initialization
	void Start () {

        //use this to select input type
        if(Application.platform == RuntimePlatform.Android  || Application.platform == RuntimePlatform.IPhonePlayer){
            isMobile = true;
        }

		ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

    void Update()
    {        
        if (!gameStarted){
            if (Input.GetMouseButtonDown(0)) { 
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(cameraRay, out hit)){
                    if (hit.transform.tag == "Ball"){
                        gameStarted = true;
                        ball.gameStarted = true;
                        ball.KickBall();
                     }
                }
            }
        }
        else {
            if(controller.startedTrick == false) {
                if (Input.GetMouseButtonDown(0)) {
                    if (!ball.canBeKicked())
                        wrongKick();
                    else
                        controller.canStartTrick = true;
                }
            }
            else {
                if (Input.GetMouseButtonUp(0) && !controller.endedTrick) {
                    controller.endedTrick = true;
                    controller.endStallTrick();
                    controller.gameOver();
                }
                    

                if (Input.GetMouseButton(0)) {               
                    ball.applyForce(controller.currentTrick.trickDirection, Input.GetAxisRaw("Mouse X") * controller.currentTrick.equilibrium);                    
                }
            }
        }
    }

    void wrongKick()
    {
        controller.endStallTrick();
        controller.gameOver();
    }
}
