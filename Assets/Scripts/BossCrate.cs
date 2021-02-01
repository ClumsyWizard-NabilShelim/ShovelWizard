using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrate : MonoBehaviour
{
	AudioManager AM;
	[HideInInspector] public bool Grabed;
	bool Hit;
	public GameObject BreakEffect;
	public float BreakDelay;

	void Start()
	{
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
	}
	void Update()
	{
		if (Hit)
		{
			if (Grabed)
			{
				BreakDelay = BreakDelay * 3;
			}
			if(BreakDelay <= 0)
			{
				DestroyCrate();
			}
			else
			{
				BreakDelay -= Time.deltaTime;
			}
		}
	}
	void OnCollisionEnter2D(Collision2D Col)
	{
		Hit = true;
		if (Col.gameObject.tag == "Player" && !Grabed)
		{
			Col.gameObject.GetComponent<Stats>().Kill();
			DestroyCrate();
		}
		else if (Col.gameObject.GetComponent<Boss>() != null)
		{
			Col.gameObject.GetComponent<Boss>().DamageBoss(10);
			DestroyCrate();
		}
	}

	void DestroyCrate()
	{
		GameObject EF = Instantiate(BreakEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Destroy(EF, 2f);
	}
}
