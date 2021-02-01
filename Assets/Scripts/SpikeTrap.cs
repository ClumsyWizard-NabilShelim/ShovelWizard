using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D Col)
	{
		if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Enemy")
		{
			if(Col.gameObject.GetComponent<Stats>() != null)
			{
				Col.gameObject.GetComponent<Stats>().Kill();
			}
		}
	}

	//void OnTriggerEnter2D(Collider2D Col)
	//{
	//	if (Col.tag == "Player" || Col.tag == "Enemy")
	//	{
	//		Destroy(Col.gameObject);
	//	}
	//}
}
