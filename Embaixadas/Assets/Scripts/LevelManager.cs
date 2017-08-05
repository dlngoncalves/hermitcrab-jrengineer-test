using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    float delay;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevelFromMenu(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadEndScreen(float waitTime)
    {
        delay = waitTime;
        StartCoroutine("LoadWithDelay");
        
    }

    IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("EndScreen");        
    }

}
