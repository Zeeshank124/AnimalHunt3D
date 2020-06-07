
using UnityEngine;

public class Target : MonoBehaviour
{
	public float health = 200f;
	public void TakeDamage(float amount)
	{
		health-=amount;
		if(health<=0f)
		{
			Die();
		}
	}
	
	void Die()
	{
		ScoreScript.scoreValue-=1;
		Destroy(gameObject);
	}

}
