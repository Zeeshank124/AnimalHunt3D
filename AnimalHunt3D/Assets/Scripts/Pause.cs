using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public static bool GameIsPaused=false;
    public GameObject pauseMenuUI;
    
    // Update is called once per frame
    void Update()
    {
	if(Input.GetKeyDown(KeyCode.Escape))
	{
		if(GameIsPaused)
		{
			Resume();
		}
		else
		{
			PauseGame();
		}
	}
        
    }
    public void Resume()
    {
	pauseMenuUI.SetActive(false);
	Time.timeScale=1f;
	GameIsPaused=false;
    }
    void PauseGame()
    {
	pauseMenuUI.SetActive(true);
	Time.timeScale= 0f;
	GameIsPaused=true;
    }
    public void LoadMenu()
    {
	Time.timeScale= 1f;
	SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
	Debug.Log("Quit!");
	Application.Quit();
    }	
}
