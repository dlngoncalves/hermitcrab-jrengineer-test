using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    int score;

	// Use this for initialization
	void Start () {
		score = 0;
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
