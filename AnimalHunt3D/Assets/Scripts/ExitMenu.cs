using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitMenu : MonoBehaviour
{
    public void Retry()
    {
	SceneManager.LoadScene("Level1");
    }
    public void ReturnToMainMenu()
    {	
	SceneManager.LoadScene("Menu");
    }
}
