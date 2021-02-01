using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactObject : MonoBehaviour
{
	public string Type;
	public GameObject Effect;
	public bool Grabed;
	[HideInInspector] public bool Breakable;
    
	void OnCollisionEnter2D(Collision2D Col)
	{
		if(Col.gameObject.tag == "Enemy")
		{
			if (Col.relativeVelocity.y < -4 || Col.relativeVelocity.y > 4 || Col.relativeVelocity.x < -4 || Col.relativeVelocity.x > 4)
			{
				Col.gameObject.GetComponent<Stats>().Kill();
				GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
				Destroy(gameObject);
				Destroy(EF, 2f);
			}
		}
		else if(Col.gameObject.tag == "Projectile")
		{
			GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(EF, 2f);
		}
		else
		{
			if (!Grabed && Breakable)
			{
				if (Col.relativeVelocity.y < -4 || Col.relativeVelocity.y > 4 || Col.relativeVelocity.x < -4 || Col.relativeVelocity.x > 4)
				{
					if (Type == "C")
					{
						GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
						Destroy(gameObject);
						Destroy(EF, 2f);
					}
				}
			}
		}
	}
}
