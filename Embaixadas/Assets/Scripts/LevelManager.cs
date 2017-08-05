using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

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

    public static void LoadLevelFromCode(string levelName, float waitTime)
    {
        
        
    }

    //IEnumerator
    //    SceneManager.LoadScene(levelName);

}
