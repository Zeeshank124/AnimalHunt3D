using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoaderScript : MonoBehaviour
{
    
     public Animator transition;
	public float transitionTime=1f;
    // Update is called once per frame
    void Update()
    {
        if(ScoreScript.scoreValue<=0)
	{
		LoadNextLevel();
	}
    }
    public void LoadNextLevel()
    {
	SceneManager.LoadScene("animalhuntkiramat");
	
    }
    
}
