using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
	public float HealthPoints
	{
		get
		{
			return healthPoints;
		}
		set
		{
			healthPoints=value;
			if(healthPoints<=0)
			{
				Destroy(gameObject);
				SceneManager.LoadScene("GameOverMenu");
			}
		}
	}
	[SerializeField]
	private float healthPoints=100f;
}
