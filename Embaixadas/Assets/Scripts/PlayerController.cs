using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    bool canKick = false;
    bool wrongKick = false;
    bool gameStarted = false;

    public float kickInterval = 2.0f;
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
            }
           
           if(canKick){
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(cameraRay, out hit)){                
                    if (hit.transform.tag == "Ball"){                    
                        ball.KickBall();
                        controller.updateScore();
                    }
                }            
            }
           else{
               Debug.Log("ERROU");
               
           }
        }
	}

    //Coisas nao utilizadas
    IEnumerator WaitToKick()
    {
        yield return new WaitForSeconds(kickInterval);
        canKick = true;
    }

    void MoveWithMouse()
    {
        Vector3 playerPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        float newPositionX = Camera.main.ScreenToWorldPoint(mousePosition).x;
        playerPosition.x = Mathf.Clamp(newPositionX, -7.0f, 7.0f);//field size
        transform.position = playerPosition;        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
            canKick = true;            
    }
}
