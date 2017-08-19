using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum TrickType
{
    Head,
    Sholder,
    Knee,
    Heel,
    Foot,
    Stall,
    Swipe
}

public enum TrickInputType
{

}


[System.Serializable]
public class Trick
{

    public TrickType type;
    [Range(0, 10)]
    public float circleDeltaSize;

    public float trickDurationTime;
    //variaveis abaixo são específicas do stall trick    
    public float equilibrium;    
    public float perturbation;
    public float startHeight; 
    public float endHeight;  
    public float deviation;
    public float trickDirection;

    //variaveis abaixo específicas do swipe trick
    public float swipeCount;
    public float[] swipeDirection;
    public float swipeSpeed;
    public float hintTime;
}



public class GameController : MonoBehaviour {

    public Text scoreText;
    int score;
    public Trick[] trickList;
    public Trick currentTrick;
    public SpriteRenderer circle;
    public bool canStartTrick = false;
    public bool startedTrick = false;
    public bool endedTrick = false;
    public Ball ball;
    Rigidbody ballBody;

    public Slider forceDirectionSlider;

    float trickTime = 0.0f;
    float startXPosition = 0.0f;

    // Use this for initialization
    void Start () {
		score = 0;
        scoreText.text = score.ToString();

        //if (circle != null)
        //   StartCoroutine(fadeIn(circle));

        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        ballBody = ball.gameObject.GetComponent<Rigidbody>();

        if (trickList.Length > 0)
            currentTrick = trickList[0];
        
    }
	
	// Update is called once per frame
	void Update () {
        if (canShowCircle()){
            StartCoroutine(fadeIn(circle));
            //hasStartedTrick = true;
        }

        if (canStartTrick && !startedTrick)
            startStallTrick();

        if (startedTrick && !endedTrick) {
            if (!ballPositionOK()) {
                endedTrick = true;
                endStallTrick();
                gameOver();
            }

            
            trickTime += Time.deltaTime;
            if (trickTime >= currentTrick.trickDurationTime) {
                endedTrick = true;
                endStallTrick();
                ball.KickBall();
                Debug.Log("WIN");
            }
                
        }
        
	}
    
    bool canShowCircle()
    {
        if (currentTrick.type != TrickType.Stall)
            return false;
        
        if ((ball.transform.position.y > circle.transform.position.y) && ball.goingDown)
            return true;
        else
            return false;
    }

    public void startStallTrick()
    {        
        ballBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        ballBody.useGravity = false;
        startXPosition = ball.transform.position.x;

        forceDirectionSlider.gameObject.SetActive(true);
        startedTrick = true;
        StartCoroutine("applyForce");
    }

    public void endStallTrick()
    {
        ballBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        ballBody.useGravity = true;
    }

    IEnumerator applyForce()//possivelmente mover isso para a classe ball
    {
        float direction = currentTrick.trickDirection * currentTrick.perturbation;
        while (!endedTrick) {
            Vector3 force = new Vector3(direction, 0.0f, 0.0f);
            ballBody.AddForce(force);
            yield return new WaitForSeconds(0.1f);
        }
    }

    bool ballPositionOK()//mover isso para a classe ball
    {
        if (Mathf.Abs(ball.transform.position.x - startXPosition) > currentTrick.deviation)
            return false;
        else
            return true;
    }

    IEnumerator fadeIn(SpriteRenderer imageToFade)
    {
        for(float t = 0.0f; t<=1.0f; t+= Time.deltaTime){

            Color newColor = imageToFade.color;
            newColor.a = t;
            imageToFade.color = newColor;            
            yield return null;
        }
    }

    void checkBallDirection()//ball method?
    {
        if (ball.transform.position.x >= 0.0)
            currentTrick.trickDirection = 1.0f;
        else
            currentTrick.trickDirection = -1.0f;
    }

    public void gameOver()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadEndScreen(2.0f);
    }
}
